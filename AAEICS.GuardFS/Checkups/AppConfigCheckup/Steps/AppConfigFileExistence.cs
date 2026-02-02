using AAEICS.GuardFS.BaseSettings;
using AAEICS.GuardFS.Contracts;
using AAEICS.GuardFS.etc;

namespace AAEICS.GuardFS.Checkups.AppConfigCheckup.Steps;

public class AppConfigFileExistence(string name, string description) : ICheckupStep
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;

    public CheckupStepResultForm Execute()
    {
        return File.Exists(AppBaseSettings.AppConfigFilePath)
            ? new CheckupStepResultForm
            {
                Result = CheckupStepResults.Successful,
                Message = $"The appsettings.json file was found at {AppBaseSettings.AppConfigFilePath}."
            }
            : new CheckupStepResultForm
            {
                Result = CheckupStepResults.Failed,
                Message = $"The appsettings.json file was not found at {AppBaseSettings.AppConfigFilePath}."
            };
    }
}