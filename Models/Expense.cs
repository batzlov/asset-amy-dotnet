using System;
using System.Collections.Generic;

namespace asset_amy.Models;

public partial class Expense
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    public string? description { get; set; }

    public double value { get; set; }

    public DateTime createdAt { get; set; }

    public DateTime updatedAt { get; set; }

    public int belongsToId { get; set; }

    public virtual User belongsTo { get; set; } = null!;
}

public class CreateExpenseDto
{
    public string name { get; set; } = null!;

    public string? description { get; set; }

    public double value { get; set; }
}

public class UpdateExpenseDto
{
    public string name { get; set; } = null!;

    public string? description { get; set; }

    public double value { get; set; }
}