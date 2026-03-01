using AAEICS.Core.DTO.General;

namespace AAEICS.Core.DTO.Certificates;

public class IssueCertificateLineDTO
{
    public int IssueLineId { get; set; }
    
    public int CertificateId { get; set; }
    
    public int OrdinalNumber { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string BatchNumber { get; set; } = null!;
    
    public int MeasureUnitId { get; set; }
    
    public MeasureUnitDTO? MeasureUnit { get; set; }
    
    public double PricePerUnit { get; set; }
    
    public decimal QuantitySent { get; set; }
    
    public CategoryDTO? CategorySent { get; set; }
    
    public decimal QuantityReceived { get; set; }
    
    public CategoryDTO? CategoryReceived { get; set; }
    
    public string? Notes { get; set; }
}
