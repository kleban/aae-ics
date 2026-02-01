using System;
using System.Collections.Generic;

namespace AaeIcs.Database.Models;

public partial class DocumentIncomingCardLine
{
    public int DocumentId { get; set; }

    public int CardLineId { get; set; }

    public virtual IncomingCardLine CardLine { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
