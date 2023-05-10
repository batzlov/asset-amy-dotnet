using System;
using System.Collections.Generic;

namespace asset_amy.Models;

public partial class User
{
    public int id { get; set; }

    public string firstName { get; set; } = null!;

    public string lastName { get; set; } = null!;

    public string email { get; set; } = null!;

    public string password { get; set; } = null!;

    public string role { get; set; } = null!;

    public DateTime createdAt { get; set; }

    public DateTime updatedAt { get; set; }

    public virtual ICollection<Asset> assets { get; set; } = new List<Asset>();

    public virtual ICollection<Expense> expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Revenue> revenues { get; set; } = new List<Revenue>();
}
