using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Api.ServiceInstallers
{
    public class MvcInstaller : IServiceInstaller
    {
        public void InstallServices (IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen (c =>
            {
                c.SwaggerDoc ("v1", new Info ());
                c.AddSecurityDefinition ("Bearer",
                   new ApiKeyScheme
                   {
                       In = "header",
                       Description = "Please enter into field the word 'Bearer' following by space and JWT",
                       Name = "Authorization",
                       Type = "apiKey"
                   });
                c.AddSecurityRequirement (new Dictionary<string, IEnumerable<string>>
               {
                    {
                       "Bearer",
                       Enumerable.Empty<string>()
                    },
               });
            });

            services.AddCors ();

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
        }
    }
}
