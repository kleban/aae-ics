using AAEICS.Core.DTO.General;

namespace AAEICS.Core.DTO.Certificates;

public class IncomingCertificateLineDTO
{
    public int? IncLineId { get; set; }

    public int? CertificateId { get; set; }

    public int OrdinalNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? NomenclatureCode { get; set; }

    public string BatchNumber { get; set; } = null!;

    public MeasureUnitDTO MeasureUnit { get; set; }

    public double PricePerUnit { get; set; }

    public decimal QuantitySent { get; set; }

    public CategoryDTO CategorySent { get; set; }

    public decimal QuantityReceived { get; set; }

    public CategoryDTO CategoryReceived { get; set; }

    public string? Notes { get; set; }

    public string? MadeIn { get; set; }
}
