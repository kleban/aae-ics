using System;
using System.Collections.Generic;

namespace AaeIcs.Database.Models;

public partial class DocumentType
{
    public int TypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
