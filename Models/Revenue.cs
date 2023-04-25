using System;
using System.Collections.Generic;

namespace asset_amy.Models;

public partial class Revenue
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double Value { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int BelongsToId { get; set; }

    public virtual User BelongsTo { get; set; } = null!;
}
