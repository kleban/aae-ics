using AAEICS.Core.DTO.AppConfig;
using Microsoft.Extensions.Configuration;

namespace AAEICS.Core.Contracts.Services;

public interface IAppConfigService
{
    IConfiguration Configuration { get; }
    
    AppConfigDTO GetAppConfig();
    string? Get(string key);
    string GetConnectionString();
    void UpdatePath(string key, string value);
}
