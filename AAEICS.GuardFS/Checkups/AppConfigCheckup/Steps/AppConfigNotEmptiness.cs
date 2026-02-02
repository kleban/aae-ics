using AAEICS.GuardFS.Contracts;
using AAEICS.GuardFS.etc;
using AAEICS.Services.AppConfig;

namespace AAEICS.GuardFS.Checkups.AppConfigCheckup.Steps;

public class AppConfigNotEmptiness(string name, string description, IAppConfigService appConfigService) : ICheckupStep
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;

    public CheckupStepResultForm Execute()
    {
        return (appConfigService.Get("Database") is null) 
            ? new CheckupStepResultForm
            {
                Result = CheckupStepResults.Failed,
                Message = "The appsettings.json file is invalid."
            }
            : new CheckupStepResultForm
            {
                Result = CheckupStepResults.Successful,
                Message = "The appsettings.json file exists and is valid."
            };
    }
}