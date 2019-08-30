using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PBS.Api.ServiceInstallers
{
    public interface IServiceInstaller
    {
        void InstallServices (IServiceCollection services, IConfiguration configuration);
    }
}
