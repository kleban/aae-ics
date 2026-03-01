using AAEICS.Core.Contracts.Services;

namespace AAEICS.Services;

public class InitialFoldersService(IAppConfigService configService) : IInitialFoldersService
{
    public void CreateInitialFolders()
    {
        var folders = configService.GetAppConfig().Path;

        if (!Directory.Exists(folders.AssetsPath))
        {
            var dir = Directory.CreateDirectory(folders.AssetsPath);
            configService.UpdatePath("AssetsPath", dir.FullName);
            configService.UpdatePath("DbPath", Path.Combine(dir.FullName, "aaeics.db"));
        }

        if (!Directory.Exists(folders.LogsPath))
        {
            var logs = Directory.CreateDirectory(folders.LogsPath);
            configService.UpdatePath("LogsPath", logs.FullName);
        }

        if (!Directory.Exists(folders.DbBackupPath))
        {
            var backup = Directory.CreateDirectory(folders.DbBackupPath);
            configService.UpdatePath("DbBackupPath", backup.FullName);
        }
    }
}
