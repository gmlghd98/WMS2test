using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models;

public partial class ProductVariant
{
    public string ProductVariantId { get; set; } = null!;

    public string? ProductId { get; set; }

    public string? VariantId { get; set; }

    public int? DisplayPosition { get; set; }

    public virtual Product? Product { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<ProductVariantComplex> ProductVariantComplexes { get; } = new List<ProductVariantComplex>();
    //[JsonIgnore]
    public virtual Variant? Variant { get; set; }
}
