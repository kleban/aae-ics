namespace AAEICS.GuardFS.BaseSettings;

public record AppBaseSettings
{
    private static readonly string RoamingBase = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AAEICS");
    
    private static readonly string LocalBase = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AAEICS");
    
    public static string AppConfigFilePath { get; } = Path.Combine(RoamingBase, "Config", "appsettings.json");

    public static string MainDbPath { get; } = Path.Combine(RoamingBase, "Database", "main_database.db");
    
    public static string LastStableDbPath { get; } = Path.Combine(LocalBase, "Database", "last_stable_database.db");
    
    public static string BackupDbPath { get; } = Path.Combine(Path.GetTempPath(), "AAEICS", "Database", "backup.db");
    
    public static string LogPath { get; } = Path.Combine(RoamingBase, "Logs", "log.txt");
    
    public static string StoragePath { get; } = Path.Combine(RoamingBase, "Storage");
}