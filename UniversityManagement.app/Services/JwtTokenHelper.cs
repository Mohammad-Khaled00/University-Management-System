using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UniversityManagement.app.Services
{
    public static class JwtTokenHelper
    {
        public static string ExtractClaimsFromToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenObj = tokenHandler.ReadJwtToken(jwtToken);
            var emailClaim = jwtTokenObj.Claims.Where(c => c.Type == "unique_name").First();
            return emailClaim?.Value;
        }
/*        public static IEnumerable<Claim> ExtractClaimsFromToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenObj = tokenHandler.ReadJwtToken(jwtToken);
            return jwtTokenObj.Claims;
        }*/
    }
}
