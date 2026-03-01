using System;
using System.Collections.Generic;

namespace AAEICS.Database.Models;

public partial class Personnel
{
    public int PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public int RankId { get; set; }

    public int PositionId { get; set; }

    public virtual ICollection<IncomingCertificate> IncomingCertificates { get; set; } = new List<IncomingCertificate>();

    public virtual ICollection<IssuanceCertificate> IssuanceCertificates { get; set; } = new List<IssuanceCertificate>();

    public virtual Position? Position { get; set; }

    public virtual Rank? Rank { get; set; }

    // public virtual ICollection<RecordCard> RecordCards { get; set; } = new List<RecordCard>();
}
