using System;
using System.Collections.Generic;

namespace WebApplication5.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string? ProductName { get; set; }

    public int? Price { get; set; }

    public int? Category { get; set; }

    public virtual Category? CategoryNavigation { get; set; }
}
