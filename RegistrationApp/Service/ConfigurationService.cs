using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
namespace RegistrationApp.Service
{
    public class ConfigurationService
    {
        public static IConfiguration Configuration { get; private set; }

        public static void Init()
        {
            if (DbProviderFactories.TryGetFactory("RegistrationAppProvider", out DbProviderFactory factory) == false)
            {
                DbProviderFactories.RegisterFactory("RegistrationAppProvider", SqlClientFactory.Instance);
            }

            if (Configuration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                Configuration = configurationBuilder.AddJsonFile("appSettings.json").Build();

            }
        }
    }
}
