
using Microsoft.AspNetCore.Identity;

namespace PJira.API.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(IdentityUser user);
    }
}
