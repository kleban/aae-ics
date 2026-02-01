using System;
using System.Collections.Generic;

namespace AaeIcs.Database.Models;

public partial class Reason
{
    public int ReasonId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual ICollection<IncomingCardLine> IncomingCardLines { get; set; } = new List<IncomingCardLine>();

    public virtual ICollection<IncomingCertificate> IncomingCertificates { get; set; } = new List<IncomingCertificate>();

    public virtual ICollection<IssuanceCertificate> IssuanceCertificates { get; set; } = new List<IssuanceCertificate>();

    public virtual ICollection<IssueCardLine> IssueCardLines { get; set; } = new List<IssueCardLine>();
}
