using AAEICS.Core.Contracts.Services.GuardFS;

namespace AAEICS.Core.DTO.GuardFS;

public record CheckupResultDTO
{
    public bool IsSuccessful { get; set; }
    public bool HasWarnings { get; set; }
    public List<ICheckupStep> SuccessfulSteps { get; set; }
    public List<ICheckupStep> FailedSteps { get; set; }
    public List<string> Messages { get; set; }
}
