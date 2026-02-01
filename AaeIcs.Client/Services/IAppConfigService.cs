using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AaeIcs.Client.Services
{
    public interface IAppConfigService
    {
        IConfiguration Configuration { get; }
        string? Get(string key);
    }
}
