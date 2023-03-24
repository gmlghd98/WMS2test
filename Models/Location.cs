using System;
using System.Collections.Generic;

namespace Models;

public partial class Location
{
    public string LocationId { get; set; } = null!;

    public string? LocationType { get; set; }

    public string? Bin { get; set; }

    public bool? Enable { get; set; }

    public string? Memo { get; set; }

    public string? Width { get; set; }

    public string? Depth { get; set; }

    public string? Height { get; set; }
}
