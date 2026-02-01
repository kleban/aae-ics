using System;
using System.Collections.Generic;

namespace AaeIcs.Database.Models;

public partial class IssueCertificateLine
{
    public int IssueLineId { get; set; }

    public int CertificateId { get; set; }

    public int OrdinalNumber { get; set; }

    public string Name { get; set; } = null!;

    public string BatchNumber { get; set; } = null!;

    public int MeasureUnit { get; set; }

    public double PricePerUnit { get; set; }

    public decimal QuantitySent { get; set; }

    public decimal CategorySent { get; set; }

    public decimal QuantityReceived { get; set; }

    public decimal CategoryReceived { get; set; }

    public string? Notes { get; set; }

    public virtual IssuanceCertificate Certificate { get; set; } = null!;

    public virtual MeasureUnit MeasureUnitNavigation { get; set; } = null!;
}
