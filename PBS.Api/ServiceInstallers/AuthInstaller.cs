using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PBS.Api.ServiceInstallers
{
    public class AuthInstaller : IServiceInstaller
    {
        public void InstallServices (IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer (options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetSection ("AppSettings:JwtIssuer").Value,
                    ValidAudience = configuration.GetSection ("AppSettings:JwtAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII
                        .GetBytes (configuration.GetSection ("AppSettings:Token").Value))
                };
            });
        }
    }
}
