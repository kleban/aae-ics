using System;
using System.Collections.Generic;

namespace AaeIcs.Database.Models;

public partial class IncomingCardLine
{
    public int CardLineId { get; set; }

    public int CardId { get; set; }

    public DateTime LineDate { get; set; }

    public int Reason { get; set; }

    public int ReferenceDocument { get; set; }

    public int Donor { get; set; }

    public int Category { get; set; }

    public decimal Quantity { get; set; }

    public string? CarBrand { get; set; }

    public string? CarNumber { get; set; }

    public virtual RecordCard Card { get; set; } = null!;

    public virtual Category CategoryNavigation { get; set; } = null!;

    public virtual ICollection<DocumentIncomingCardLine> DocumentIncomingCardLines { get; set; } = new List<DocumentIncomingCardLine>();

    public virtual TransferInstance DonorNavigation { get; set; } = null!;

    public virtual Reason ReasonNavigation { get; set; } = null!;
}
