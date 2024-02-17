using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniversityManagement.Core.Repositories;
using UniversityManagement.Core.Servicces;

namespace UniversityManagement.app.Services
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {

        private readonly IUserRepository _userRepository;

        public JwtAuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool EmailExists(string email)
        {
            return _userRepository.EmailExists(email);
        }

        public string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("Y5ntAqD3bFzR6UyeG9GmE3DLYWTp9Shs");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email),
                    // Any additional claims
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
