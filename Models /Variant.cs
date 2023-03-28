using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Models;

public partial class Variant
{
    public string VariantId { get; set; } = null!;

    public string? VariantName { get; set; }

    public int? DisplayPosition { get; set; }

    [JsonIgnore]
    public virtual ICollection<ProductVariant> ProductVariants { get; } = new List<ProductVariant>();
    [JsonIgnore]
    public virtual ICollection<VariantValue> VariantValues { get; } = new List<VariantValue>();
}
