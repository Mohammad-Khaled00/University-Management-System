using Microsoft.AspNetCore.Identity;

namespace UniversityManagement.Core.Servicces
{
    public interface IAccountService
    {
        Task<(bool SignInResult, IList<string> Roles, IdentityUser User)> LoginAsync(string email, string password, bool rememberMe);

        string GenerateJwtToken(IdentityUser user, IList<string> roles);
    }
}
