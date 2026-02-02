using System;
using System.Collections.Generic;

namespace AAEICS.Database.Models;

public partial class DocumentIssueCardLine
{
    public int DocumentId { get; set; }

    public int CardLineId { get; set; }

    public virtual IssueCardLine CardLine { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
