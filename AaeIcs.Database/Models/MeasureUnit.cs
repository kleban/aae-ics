using System;
using System.Collections.Generic;

namespace AaeIcs.Database.Models;

public partial class MeasureUnit
{
    public int UnitId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<IncomingCertificateLine> IncomingCertificateLines { get; set; } = new List<IncomingCertificateLine>();

    public virtual ICollection<IssueCertificateLine> IssueCertificateLines { get; set; } = new List<IssueCertificateLine>();
}
