namespace AAEICS.Core.DTO.General;

public class PersonnelDTO
{
    public int PersonId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public RankDTO Rank { get; set; }
    public PositionDTO Position { get; set; }
}
