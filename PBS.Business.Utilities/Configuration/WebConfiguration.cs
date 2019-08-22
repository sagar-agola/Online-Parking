using Microsoft.Extensions.Configuration;
using System.IO;

namespace PBS.Business.Utilities.Configuration
{
    public class WebConfiguration : IWebConfiguration
    {
        public string SenderName
        {
            get
            {
                IConfigurationRoot root = GetConfigurationRoot ();

                return root.GetSection ("AppSettings:SenderName").Value;
            }
        }

        public string Email
        {
            get
            {
                IConfigurationRoot root = GetConfigurationRoot ();

                return root.GetSection ("AppSettings:Email").Value;
            }
        }

        public string Password
        {
            get
            {
                IConfigurationRoot root = GetConfigurationRoot ();

                return root.GetSection ("AppSettings:Password").Value;
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
