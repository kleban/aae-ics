namespace AAEICS.Core.DTO.RecordCards;

public class RecordCardDTO
{
    public int CardId { get; set; }
    
    public int TransferLine { get; set; }
    
    public int? RegisterNumber { get; set; }
    
    public string ArmyBaseName { get; set; } = null!;
    
    public string MinisterialName { get; set; } = null!;
    
    public int ResponsiblePerson { get; set; }
    
    public bool IsRestrictToDispatch { get; set; }
    
    public int? ManagementCompany { get; set; }
    
    public int? DeliveryCentre { get; set; }
    
    public decimal? MaxAmount { get; set; }
    
    public decimal? MinAmount { get; set; }
    
    public int? Storage { get; set; }
    
    public int? Shelf { get; set; }
    
    public int? Rack { get; set; }
    
    public int? Container { get; set; }
}
