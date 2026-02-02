using AAEICS.GuardFS.Contracts;

namespace AAEICS.GuardFS.etc;

public record CheckupResultForm
{
    public bool IsSuccessful { get; set; }
    public bool HasWarnings { get; set; }

    public List<ICheckupStep> SuccessfulSteps { get; set; }
    public List<ICheckupStep> FailedSteps { get; set; }
    public List<string> Messages { get; set; }
}