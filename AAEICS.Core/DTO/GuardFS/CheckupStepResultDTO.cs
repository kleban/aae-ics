using AAEICS.Core.Enums;

namespace AAEICS.Core.DTO.GuardFS;

public record CheckupStepResultDTO
{
    public CheckupStepResults Result { get; init; }
    public string? Message { get; init; }
}
