using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Core.Servicces;

namespace University_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtAuthenticationService jwt;

        public LoginController(IJwtAuthenticationService JWT)
        {
            jwt = JWT;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email is required.");
            }

            bool emailExists = jwt.EmailExists(request.Email);

            if (!emailExists)
            {
                return NotFound("User with provided email not found.");
            }

            // Generate JWT token
            string token = jwt.GenerateJwtToken(request.Email);

            return Ok(new { token });
        }
    }
}
