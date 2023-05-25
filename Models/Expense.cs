using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Pflichtfeld")]
    public string name { get; set; } = null!;

    public string? description { get; set; }

    [Required(ErrorMessage = "Pflichtfeld")]
    [Range(0, 9999999999999999.99)]
    public double? value { get; set; }
}

public class UpdateExpenseDto
{
    [Required(ErrorMessage = "Pflichtfeld")]
    public string name { get; set; } = null!;

    public string? description { get; set; }

    [Required(ErrorMessage = "Pflichtfeld")]
    [Range(0, 9999999999999999.99)]
    public double? value { get; set; }
}