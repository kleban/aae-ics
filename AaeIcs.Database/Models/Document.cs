using System;
using System.Collections.Generic;

namespace AAEICS.Database.Models;

public partial class Document
{
    public int DocumentId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? Date { get; set; }

    public int? Type { get; set; }

    public virtual DocumentIncomingCardLine? DocumentIncomingCardLine { get; set; }

    public virtual DocumentIssueCardLine? DocumentIssueCardLine { get; set; }

    public virtual DocumentType? TypeNavigation { get; set; }
}
