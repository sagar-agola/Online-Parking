using Microsoft.Extensions.Configuration;
using System.IO;

namespace PBS.Business.Utilities.Configuration
{
    public class ApiConfiguration : IApiConfiguration
    {
        public string Token
        {
            get
            {
                IConfigurationRoot root = GetConfigurationRoot();

                return root.GetSection ("AppSettings:Token").Value;
            }
        }

        public string Issuer
        {
            get
            {
                IConfigurationRoot root = GetConfigurationRoot ();

                return root.GetSection ("AppSettings:JwtIssuer").Value;
            }
        }

        public string Audience
        {
            get
            {
                IConfigurationRoot root = GetConfigurationRoot ();

                return root.GetSection ("AppSettings:JwtAudience").Value;
            }
        }

        private IConfigurationRoot GetConfigurationRoot ()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder ();
            string path = Path.Combine (Directory.GetCurrentDirectory (), "appsettings.json");
            configurationBuilder.AddJsonFile (path, false);

            return configurationBuilder.Build ();
        }
    }
}
