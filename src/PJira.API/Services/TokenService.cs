using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PJira.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateToken(IdentityUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));

            var credetials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("userName", user.UserName)

            };

            var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpirationInMinutes"])),
                    signingCredentials: credetials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
