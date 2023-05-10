using System;
using System.Collections.Generic;

namespace asset_amy.Models;

public partial class Asset
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    public string type { get; set; } = null!;

    public string description { get; set; } = null!;

    public double value { get; set; }

    public DateTime createdAt { get; set; }

    public DateTime updatedAt { get; set; }

    public int belongsToId { get; set; }

    public virtual User belongsTo { get; set; } = null!;
}
