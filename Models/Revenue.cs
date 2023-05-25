using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asset_amy.Models;

public partial class Revenue
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    public string description { get; set; } = null!;

    public double value { get; set; }

    public DateTime createdAt { get; set; }

    public DateTime updatedAt { get; set; }

    public int belongsToId { get; set; }

    public virtual User belongsTo { get; set; } = null!;
}

public class CreateRevenueDto
{
    [Required(ErrorMessage = "Pflichtfeld")]
    public string name { get; set; } = null!;

    public string description { get; set; } = null!;

    [Required(ErrorMessage = "Pflichtfeld")]
    [Range(0, 9999999999999999.99)]
    public double? value { get; set; }
}

public class UpdateRevenueDto
{
    [Required(ErrorMessage = "Pflichtfeld")]
    public string name { get; set; } = null!;

    public string description { get; set; } = null!;

    [Required(ErrorMessage = "Pflichtfeld")]
    [Range(0, 9999999999999999.99)]
    public double? value { get; set; }
}