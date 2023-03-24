using System;
using System.Collections.Generic;

namespace Models;

public partial class VariantValue
{
    public string VariantValueId { get; set; } = null!;

    public string? VariantId { get; set; }

    public string? Value { get; set; }

    public int? DisplayPosition { get; set; }

    public virtual ICollection<ProductVariantComplex> ProductVariantComplexes { get; } = new List<ProductVariantComplex>();

    public virtual Variant? Variant { get; set; }
}
