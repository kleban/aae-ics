using System;
using System.Collections.Generic;

namespace AAEICS.Database.Models;

public partial class IncomingCertificateLine
{
    public int IncLineId { get; set; }

    public int CertificateId { get; set; }

    public int OrdinalNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? NomenclatureCode { get; set; }

    public string BatchNumber { get; set; } = null!;

    public int MeasureUnitId { get; set; }

    public double PricePerUnit { get; set; }

    public decimal QuantitySent { get; set; }

    public int CategorySentId { get; set; }

    public decimal QuantityReceived { get; set; }

    public int CategoryReceivedId { get; set; }

    public string? Notes { get; set; }

    public string? MadeIn { get; set; }

    public virtual IncomingCertificate Certificate { get; set; } = null!;

    public virtual MeasureUnit MeasureUnit { get; set; } = null!;
    
    public virtual Category CategorySent { get; set; } = null!;
    
    public virtual Category CategoryReceived { get; set; } = null!;

    // public virtual ICollection<RecordCard> RecordCards { get; set; } = new List<RecordCard>();
}
