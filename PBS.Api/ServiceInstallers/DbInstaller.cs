using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PBS.Database.Context;

namespace PBS.Api.ServiceInstallers
{
    public class DbInstaller : IServiceInstaller
    {
        public void InstallServices (IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PbsDbContext> (item =>
            {
                item.UseSqlServer (configuration.GetConnectionString ("DBCS"));
            });
        }
    }
}
