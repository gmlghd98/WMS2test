using System;
using System.Collections.Generic;

namespace Models;

public partial class Transaction
{
    public string TransactionId { get; set; } = null!;

    public string? InventoryId { get; set; }

    public string? TransactionType { get; set; }

    public string? BoardId { get; set; }

    public int? ProductQty { get; set; }

    public string? ProductId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Board? Board { get; set; }

    public virtual Inventory? Inventory { get; set; }

    public virtual ICollection<TransactionHandlingUnit> TransactionHandlingUnits { get; } = new List<TransactionHandlingUnit>();
}
