using System;
using System.Collections.Generic;

namespace Models;

public partial class Inventory
{
    public string InventoryId { get; set; } = null!;

    public string? Sku { get; set; }

    public string? Barcode { get; set; }

    public int? CurrentQty { get; set; }

    public string? ProductId { get; set; }

    public virtual ICollection<InventoryComplex> InventoryComplexes { get; } = new List<InventoryComplex>();

    public virtual Product? Product { get; set; }

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
