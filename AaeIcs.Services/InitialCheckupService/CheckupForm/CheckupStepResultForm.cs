namespace AAEICS.Services.InitialCheckupService.CheckupForm;

public record CheckupStepResultForm
{
    public CheckupStepResults Result { get; init; }
    public string? Message { get; init; }
}
