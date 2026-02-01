using System;
using System.Collections.Generic;

namespace AaeIcs.Database.Models;

public partial class Rank
{
    public int RankId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Personnel> Personnel { get; set; } = new List<Personnel>();
}
