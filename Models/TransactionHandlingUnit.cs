using System;
using System.Collections.Generic;

namespace Models;

public partial class TransactionHandlingUnit
{
    public string TransactionHandlingUnitId { get; set; } = null!;

    public string? TransactionId { get; set; }

    public string? TransactionType { get; set; }

    public int? HandlingUnitQty { get; set; }

    public string? ParentHandlingUnitId { get; set; }

    public string? Bin { get; set; }

    public string? HandlingUnitId { get; set; }

    public virtual HandlingUnit? HandlingUnit { get; set; }

    public virtual Transaction? Transaction { get; set; }
}
