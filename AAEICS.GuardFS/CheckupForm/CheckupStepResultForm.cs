namespace AAEICS.GuardFS.etc;

public record CheckupStepResultForm
{
    public CheckupStepResults Result { get; init; }
    public string? Message { get; init; }
}
