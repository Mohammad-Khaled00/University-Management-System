using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Core.Servicces;

namespace University_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService acc;

        public LoginController(IAccountService Acc)
        {
            acc = Acc;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest("Email is required.");

            if (string.IsNullOrEmpty(request.Password))
                return BadRequest("Please enter the password.");

            var signInResult = await acc.LoginAsync(request.Email, request.Password, rememberMe: false);

            if (signInResult.SignInResult)
            {
                string token = acc.GenerateJwtToken(signInResult.User, signInResult.Roles);
                return Ok(new { token, signInResult.Roles});
            }

            else
                return BadRequest("Invalid email or password.");
        }
    }
}
