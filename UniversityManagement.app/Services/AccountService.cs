using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniversityManagement.Core.Servicces;

namespace UniversityManagement.app.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<(bool SignInResult, IList<string> Roles, IdentityUser User)> LoginAsync(string email, string password, bool rememberMe)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, password))
                return (false, null, null);

            await _signInManager.SignInAsync(user, false);
            IList<string> rolesList = await _userManager.GetRolesAsync(user);
            return (true, rolesList, user);
        }

        public string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("Y5ntAqD3bFzR6UyeG9GmE3DLYWTp9Shs");

            // Create a list to store all claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Add a claim for each role in the roles list
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
