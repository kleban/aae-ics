using AAEICS.Services.InitialCheckupService.Contracts;

namespace AAEICS.Services.InitialCheckupService.CheckupForm;

public record CheckupResultForm
{
    public bool IsSuccessful { get; set; }
    public bool HasWarnings { get; set; }

    public List<ICheckupStep> SuccessfulSteps { get; set; }
    public List<ICheckupStep> FailedSteps { get; set; }
    public List<string> Messages { get; set; }
}