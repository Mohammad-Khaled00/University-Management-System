using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace University_Management_System
{
    public class Startup
    {
        public static void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            //app.UseMiddleware<LoggingMiddleware>();
        }
        public static void ConfigureServices(IServiceCollection services)
        {

            // authentication services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:7074/",
                    ValidAudience = "https://localhost:7074/api",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Y5ntAqD3bFzR6UyeG9GmE3DLYWTp9Shs"))
                };
            });
            services.AddAuthorization();
        }

    }
}
