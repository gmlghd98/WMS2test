using System;
using System.Collections.Generic;

namespace Models;

public partial class HandlingUnitType
{
    public string HandlingUnitTypeId { get; set; } = null!;

    public string? HandlingUnitTypeName { get; set; }

    public bool? Fixed { get; set; }

    public decimal? Width { get; set; }

    public decimal? Depth { get; set; }

    public decimal? Height { get; set; }

    public int? TypeLevel { get; set; }

    public string? LabelFormat { get; set; }

    public virtual ICollection<HandlingUnit> HandlingUnits { get; } = new List<HandlingUnit>();
}
