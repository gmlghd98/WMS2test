using System;
using System.Collections.Generic;

namespace Models;

public partial class Variant
{
    public string VariantId { get; set; } = null!;

    public string? VariantName { get; set; }

    public int? DisplayPosition { get; set; }

    public virtual ICollection<ProductVariant> ProductVariants { get; } = new List<ProductVariant>();

    public virtual ICollection<VariantValue> VariantValues { get; } = new List<VariantValue>();
}
