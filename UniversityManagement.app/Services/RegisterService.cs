using Microsoft.AspNetCore.Identity;
using UniversityManagement.Core.Servicces;
using UniversityManagement.Core.ViewModels;

namespace UniversityManagement.app.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAccountService jwt;

        public RegisterService(UserManager<IdentityUser> userManager, IAccountService JWT)
        {
            _userManager = userManager;
            jwt = JWT;
        }

        public async Task<ResponseEntity<UserVM>> RegisterAsync(AuthVM model, string Role)
        {
            model.Username = model.Username.Replace(" ", "-");
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new ResponseEntity<UserVM> { Errors = ["Email is already registered!"] };

            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new ResponseEntity<UserVM> { Errors = ["Username is already registered!"] };

            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new ResponseEntity<UserVM> { Errors = [errors] };
            }
            await _userManager.AddToRoleAsync(user, $"{Role}");
            var jwtSecurityToken = jwt.GenerateJwtToken(user, [Role]);
            UserVM VM = new() { Id = user.Id, Email = user.Email };
            return new ResponseEntity<UserVM>
            {
                Data = VM,
                StatusCode = 200,
                Errors = null
            };
        }
    }
}
