using System;
using System.Collections.Generic;

namespace AaeIcs.Database.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<IncomingCardLine> IncomingCardLines { get; set; } = new List<IncomingCardLine>();

    public virtual ICollection<IssueCardLine> IssueCardLines { get; set; } = new List<IssueCardLine>();
}
