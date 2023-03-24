using System;
using System.Collections.Generic;

namespace Models;

public partial class ProductVariantComplex
{
    public string VariantComplexId { get; set; } = null!;

    public string? ProductVariantId { get; set; }

    public string? VariantValueId { get; set; }

    public virtual ICollection<InventoryComplex> InventoryComplexes { get; } = new List<InventoryComplex>();

    public virtual ProductVariant? ProductVariant { get; set; }

    public virtual VariantValue? VariantValue { get; set; }
}
