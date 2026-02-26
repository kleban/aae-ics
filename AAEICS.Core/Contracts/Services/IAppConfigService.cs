using AAEICS.Core.DTO.AppConfig;
using Microsoft.Extensions.Configuration;

namespace AAEICS.Core.Contracts.Services;

public interface IAppConfigService
{
    AppConfigDTO GetAppConfig();
    IConfiguration Configuration { get; }
    string? Get(string key);
    string GetConnectionString();
    void UpdatePath(string key, string value);
}

