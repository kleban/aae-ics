using System;
using System.Collections.Generic;

namespace AAEICS.Database.Models;

public partial class RecordCard
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

    // public virtual TransferInstance? DeliveryCentreNavigation { get; set; }
    //
    // public virtual ICollection<IncomingCardLine> IncomingCardLines { get; set; } = new List<IncomingCardLine>();
    //
    // public virtual ICollection<IssueCardLine> IssueCardLines { get; set; } = new List<IssueCardLine>();
    //
    // public virtual TransferInstance? ManagementCompanyNavigation { get; set; }

    public virtual Personnel ResponsiblePersonNavigation { get; set; } = null!;

    public virtual IncomingCertificateLine TransferLineNavigation { get; set; } = null!;
}
