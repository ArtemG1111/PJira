
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PJira.API.Services;
using PJira.Application.DTOs;



namespace PJira.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager
            , IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserDto userDto)
        {

            var identityUser = _mapper.Map<IdentityUser>(userDto);

            var result = await _userManager.CreateAsync(identityUser, userDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(identityUser, false);

            var token = _tokenService.GenerateToken(_mapper.Map<IdentityUser>(userDto));

            return Ok(token);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn([FromQuery] UserDto userDto)
        {

            var user = await _userManager.FindByNameAsync(userDto.UserName);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            await _signInManager.PasswordSignInAsync(user, userDto.Password, false, false);

            var token = await _tokenService.GenerateToken(_mapper.Map<IdentityUser>(userDto));

            return Ok(token);
        }
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            if (User.Identity?.Name == null)
            {
                return BadRequest("You have to sign in first");
            }
            await _signInManager.SignOutAsync();

            return Ok("Successfully log out");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }
    }
}
