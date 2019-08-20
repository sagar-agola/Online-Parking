using Microsoft.Extensions.Configuration;
using System.IO;

namespace PBS.Business.Utilities.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        public string Token
        {
            get
            {
                ConfigurationBuilder configurationBuilder = new ConfigurationBuilder ();
                string path = Path.Combine (Directory.GetCurrentDirectory (), "appsettings.json");
                configurationBuilder.AddJsonFile (path, false);

                IConfigurationRoot root = configurationBuilder.Build ();

                return root.GetSection ("AppSettings:Token").Value;
            }
        }
    }
}
