using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PBS.Api.ServiceInstallers
{
    public static class InstallerExtensions
    {
        public static void InstallServices (this IServiceCollection services, IConfiguration configuration)
        {
            List<IServiceInstaller> installers = typeof (Startup).Assembly.ExportedTypes
                .Where (x => typeof (IServiceInstaller)
                .IsAssignableFrom (x) && !x.IsInterface && !x.IsAbstract)
                .Select (Activator.CreateInstance)
                .Cast<IServiceInstaller> ()
                .ToList ();

            installers.ForEach (installer => installer.InstallServices (services, configuration));
        }
    }
}
