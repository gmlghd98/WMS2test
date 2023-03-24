using System;
using System.Collections.Generic;

namespace Models;

public partial class ProductVariant
{
    public string ProductVariantId { get; set; } = null!;

    public string? ProductId { get; set; }

    public string? VariantId { get; set; }

    public int? DisplayPosition { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ICollection<ProductVariantComplex> ProductVariantComplexes { get; } = new List<ProductVariantComplex>();

    public virtual Variant? Variant { get; set; }
}
