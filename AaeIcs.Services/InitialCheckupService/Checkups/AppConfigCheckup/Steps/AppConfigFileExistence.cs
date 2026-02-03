using AAEICS.Services.InitialCheckupService.CheckupForm;
using AAEICS.Services.InitialCheckupService.Contracts;

namespace AAEICS.Services.InitialCheckupService.Checkups.AppConfigCheckup.Steps;

public class AppConfigFileExistence(string name, string description, string appsettings_path) : ICheckupStep
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;

    public CheckupStepResultForm Execute()
    {
        return File.Exists(appsettings_path)
            ? new CheckupStepResultForm
            {
                Result = CheckupStepResults.Successful,
                Message = $"The appsettings.json file was found at {appsettings_path}."
            }
            : new CheckupStepResultForm
            {
                Result = CheckupStepResults.Failed,
                Message = $"The appsettings.json file was not found at {appsettings_path}."
            };
    }
}