using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? ProductCategoryId { get; set; }

    public string? ItemCode { get; set; }

    public int? BillingUnit { get; set; }

    public int? BillingQty { get; set; }

    public virtual ICollection<Inventory> Inventories { get; } = new List<Inventory>();

    public virtual ProductCategory? ProductCategory { get; set; }
    [JsonIgnore]
    public virtual ICollection<ProductVariant> ProductVariants { get; } = new List<ProductVariant>();
}
