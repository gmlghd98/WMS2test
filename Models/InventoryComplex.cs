using System;
using System.Collections.Generic;

namespace Models;

public partial class InventoryComplex
{
    public string InventoryId { get; set; } = null!;

    public string VariantComplexId { get; set; } = null!;

    public int InventoryComplexId { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual ProductVariantComplex VariantComplex { get; set; } = null!;
}
