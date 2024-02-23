using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using UniversityManagement.Core.Repositories;

namespace UniversityManagement.Infrastructure.Repositories
{
    public class FetchTokenRepo : IFetchTokenRepo
    {
        private readonly IHttpContextAccessor context;

        public FetchTokenRepo(IHttpContextAccessor httpContext)
        {
            context = httpContext;
        }

        public string FetchClaims()
        {
            var tokenValues = context.HttpContext.Request.Headers["Authorization"];
            if (tokenValues.Count > 0)
            {
                string token = tokenValues[0];
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtTokenObj = tokenHandler.ReadJwtToken(token.Replace("Bearer ", ""));
                var emailClaim = jwtTokenObj.Claims.Where(c => c.Type == "email").First();
                return emailClaim?.Value;
            }
            return null;
        }
        /*        public static IEnumerable<Claim> ExtractClaimsFromToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenObj = tokenHandler.ReadJwtToken(jwtToken);
            return jwtTokenObj.Claims;
        }*/
    }
}
