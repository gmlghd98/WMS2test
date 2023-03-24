using System;
using System.Collections.Generic;

namespace Models;

public partial class BoardListValue
{
    public string BoardListValueId { get; set; } = null!;

    public string? BoardListId { get; set; }

    public string? BoardFieldId { get; set; }

    public string? Value { get; set; }

    public virtual BoardField? BoardField { get; set; }

    public virtual BoardList? BoardList { get; set; }
}
