using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AAEICS.Services.AppConfig
{
    public interface IAppConfigService
    {
        IConfiguration Configuration { get; }
        string? Get(string key);
    }
}
