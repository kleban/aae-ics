using Microsoft.Extensions.Configuration;

namespace AAEICS.Services.AppConfig
{
    public class AppConfigService : IAppConfigService
    {
        public IConfiguration Configuration { get; }

        public AppConfigService()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AAEICS", "Config")
                )
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public string? Get(string key) => Configuration[key];
    }
}
