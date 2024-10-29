
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager
            , IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SingUp(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var identityUser = _mapper.Map<IdentityUser>(userDto);

                var result = await _userManager.CreateAsync(identityUser, userDto.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                await _signInManager.SignInAsync(identityUser, false);
            }
            return Ok("User was create");
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> SignIn([FromQuery] UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userDto.UserName);

                if (user == null)
                {
                    return BadRequest("User not found");
                }
                var result = await _signInManager.PasswordSignInAsync(user, userDto.Password, false, false);

                if (!result.Succeeded)
                {
                    return BadRequest("Invalid name or password");
                }
            }
            return Ok("Successfully signed in");
        }
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> SingOut(UserDto userDto)
        {
            if (User.Identity?.Name == null)
            {
                return BadRequest("You have to sign in first");
            }
            await _signInManager.SignOutAsync();

            return Ok("Successfully signed out");
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = _userManager.Users.ToList();
            return Ok(result);
        }
    }
}
