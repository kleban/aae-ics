namespace AAEICS.Core.DTO.AppConfig;

public sealed class AppConfigDTO
{
    public KeysSection Keys { get; set; } = new();
    public PathSection Path { get; set; } = new();
}
