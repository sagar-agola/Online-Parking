using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PBS.Business.Utilities.Configuration;
using PBS.Business.Utilities.Helpers;

namespace PBS.Api.ServiceInstallers
{
    public class UtilitiesInstaller : IServiceInstaller
    {
        public void InstallServices (IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IApiConfiguration, ApiConfiguration> ();
            services.AddSingleton<ITokenManager, TokenManager> ();
        }
    }
}
