using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAEICS.Services.AppConfiguration
{
    public interface IAppConfigService
    {
        AppConfig GetAppConfig();
        IConfiguration Configuration { get; }
        string? Get(string key);
        string GetConnectionString();
        void UpdatePath(string key, string value);
    }
}
