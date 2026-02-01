using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AaeIcs.Client.Services
{
    public class AppConfigService : IAppConfigService
    {
        public IConfiguration Configuration { get; }

        public AppConfigService()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public string? Get(string key) => Configuration[key];
    }
}
