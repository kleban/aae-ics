using AAEICS.Services.AppConfiguration;

namespace AAEICS.Services.InitialFolders;

public class InitialFoldersService(IAppConfigService configService) : IInitialFoldersService
{
    public void CreateInitialFolders()
    {
        var folders = configService.GetAppConfig().Path;

        if (!System.IO.Directory.Exists(folders.AssetsPath))
        {
            var dir = Directory.CreateDirectory(folders.AssetsPath);
            configService.UpdatePath("AssetsPath", dir.FullName);
            configService.UpdatePath("DbPath", Path.Combine(dir.FullName, "aaeics.db"));
        }

        if (!System.IO.Directory.Exists(folders.LogsPath))
        {
            var logs = System.IO.Directory.CreateDirectory(folders.LogsPath);
            configService.UpdatePath("LogsPath", logs.FullName);
        }

        if (!System.IO.Directory.Exists(folders.DbBackupPath))
        {
            var backup = System.IO.Directory.CreateDirectory(folders.DbBackupPath);
            configService.UpdatePath("DbBackupPath", backup.FullName);
        }
    }
}
