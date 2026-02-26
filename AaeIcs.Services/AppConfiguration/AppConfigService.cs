using AAEICS.Core.Contracts;
using AAEICS.Core.DTO.AppConfig;

using Microsoft.Extensions.Configuration;

using System.Text;
using System.Text.Json;
using AAEICS.Core.Contracts.Services;

namespace AAEICS.Services.AppConfiguration;

public class AppConfigService : IAppConfigService
{
    private const string ConfigFileName = "appsettings.json";

    private readonly string _configPath;

    public IConfiguration Configuration { get; private set; }

    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        WriteIndented = true
    };

    public AppConfigService()
    {
        _configPath = Path.Combine(AppContext.BaseDirectory, ConfigFileName);
        ReloadConfiguration();
    }

    private void ReloadConfiguration()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(ConfigFileName, optional: true, reloadOnChange: true)
            .Build();
    }

    public AppConfigDTO GetAppConfig()
    {
        if (!File.Exists(_configPath))
            return new AppConfigDTO(); 

        var json = File.ReadAllText(_configPath, Encoding.UTF8);

        var cfg = JsonSerializer.Deserialize<AppConfigDTO>(json, Options);
        return cfg ?? new AppConfigDTO();
    }

    public string? Get(string key) => Configuration[key];

    public string GetConnectionString()
    {
        var dbRelPath = Configuration["Path:DbPath"] ?? @"assets\aaeics_db.db";
        var full = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, dbRelPath));
        return $"Data Source={full}";
    }

    public void UpdatePath(string key, string value)
    {
        var cfg = GetAppConfig();
        switch (key)
        {
            case nameof(PathSection.AssetsPath):
                cfg.Path.AssetsPath = value;
                break;
            case nameof(PathSection.DbPath):
                cfg.Path.DbPath = value;
                break;
            case nameof(PathSection.DbBackupPath):
                cfg.Path.DbBackupPath = value;
                break;
            case nameof(PathSection.LogsPath):
                cfg.Path.LogsPath = value;
                break;
            default:
                throw new ArgumentException($"Unknown path key: {key}");
        }

        SaveAppConfig(cfg);
        ReloadConfiguration();
    }

    private void SaveAppConfig(AppConfigDTO cfg)
    {
        var json = JsonSerializer.Serialize(cfg, Options);
        var tmp = _configPath + ".tmp";
        File.WriteAllText(tmp, json, Encoding.UTF8);
        File.Copy(tmp, _configPath, overwrite: true);
        File.Delete(tmp);
    }

}
