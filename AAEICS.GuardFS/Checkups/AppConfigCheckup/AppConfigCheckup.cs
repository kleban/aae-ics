using AAEICS.GuardFS.Checkups.AppConfigCheckup.Steps;

using AAEICS.GuardFS.Contracts;
using AAEICS.GuardFS.etc;
using AAEICS.Services.AppConfig;

namespace AAEICS.GuardFS.Checkups.AppConfigCheckup;

public class AppConfigCheckup(string name, string description) : ICheckup
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;

    private readonly List<ICheckupStep> _steps =
    [
        new AppConfigFileExistence("App Settings Existence",
            "Checks if the appsettings.json file in AppData directory exists"),
        new AppConfigRawValidator("App Settings Not Empty", 
            "Checks if the appsettings.json file is not empty"
            )
    ];

    private bool _hasWarnings;
    private List<ICheckupStep> _successfulSteps = [];
    private List<ICheckupStep> _failedSteps = [];
    private List<string> _messages = [];
    
    public CheckupResultForm Execute()
    {
        foreach (var step in _steps)
        {
            var stepResultForm = step.Execute();
            if (stepResultForm.Result is CheckupStepResults.Failed)
            {
                _failedSteps.Add(step);
                if (!string.IsNullOrEmpty(stepResultForm.Message))
                    _messages.Add(stepResultForm.Message);
                
                return new CheckupResultForm
                {
                    IsSuccessful = false,
                    HasWarnings = _hasWarnings,
                    SuccessfulSteps = _successfulSteps,
                    FailedSteps = _failedSteps,
                    Messages = _messages
                };
            }

            
            if (!_hasWarnings && stepResultForm.Result is CheckupStepResults.Warning)
                _hasWarnings = true;
            
            if (stepResultForm.Result is CheckupStepResults.Successful)
                _successfulSteps.Add(step);
            else
                _failedSteps.Add(step);
            
            if (!string.IsNullOrEmpty(stepResultForm.Message))
                _messages.Add(stepResultForm.Message);
        }

        return new CheckupResultForm
        {
            IsSuccessful = _failedSteps.Count == 0,
            HasWarnings = _hasWarnings,
            SuccessfulSteps = _successfulSteps,
            FailedSteps = _failedSteps,
            Messages = _messages
        };
    }
}