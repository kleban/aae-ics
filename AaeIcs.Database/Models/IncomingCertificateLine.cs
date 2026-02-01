using System;
using System.Collections.Generic;

namespace AaeIcs.Database.Models;

public partial class IncomingCertificateLine
{
    public int IncLineId { get; set; }

    public int CertificateId { get; set; }

    public int OrdinalNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? NomenclatureCode { get; set; }

    public string BatchNumber { get; set; } = null!;

    public int MeasureUnit { get; set; }

    public double PricePerUnit { get; set; }

    public decimal QuantitySent { get; set; }

    public decimal CategorySent { get; set; }

    public decimal QuantityReceived { get; set; }

    public decimal CategoryReceived { get; set; }

    public string? Notes { get; set; }

    public string? MadeIn { get; set; }

    public virtual IncomingCertificate Certificate { get; set; } = null!;

    public virtual MeasureUnit MeasureUnitNavigation { get; set; } = null!;

    public virtual ICollection<RecordCard> RecordCards { get; set; } = new List<RecordCard>();
}
