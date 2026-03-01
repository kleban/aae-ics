namespace AAEICS.Core.DTO.AppConfig;

public sealed class PathSection
{
    public string DbPath { get; set; } = "";
    public string AssetsPath { get; set; } = "";
    public string DbBackupPath { get; set; } = "";
    public string LogsPath { get; set; } = "";
}
