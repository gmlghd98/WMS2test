using System;
using System.Collections.Generic;

namespace Models;

public partial class HandlingUnit
{
    public string HandlingUnitId { get; set; } = null!;

    public string? HandlingUnitTypeId { get; set; }

    public string? HandlingUnitNumber { get; set; }

    public string? ParentHandlingUnitId { get; set; }

    public string? CurrentBin { get; set; }

    public int? CurrentProductQty { get; set; }

    public virtual HandlingUnitType? HandlingUnitType { get; set; }

    public virtual ICollection<TransactionHandlingUnit> TransactionHandlingUnits { get; } = new List<TransactionHandlingUnit>();
}
