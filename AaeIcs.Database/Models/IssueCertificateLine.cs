using System;
using System.Collections.Generic;

namespace AAEICS.Database.Models;

public partial class IssueCertificateLine
{
    public int IssueLineId { get; set; }

    public int CertificateId { get; set; }

    public int OrdinalNumber { get; set; }

    public string Name { get; set; } = null!;

    public string BatchNumber { get; set; } = null!;

    public int MeasureUnitId { get; set; }

    public double PricePerUnit { get; set; }

    public decimal QuantitySent { get; set; }

    public int CategorySentId { get; set; }

    public decimal QuantityReceived { get; set; }

    public int CategoryReceivedId { get; set; }

    public string? Notes { get; set; }

    public virtual IssuanceCertificate Certificate { get; set; } = null!;

    public virtual MeasureUnit MeasureUnit { get; set; } = null!;

    public virtual Category CategorySent { get; set; } = null!;
    
    public virtual Category CategoryReceived { get; set; } = null!;
}
