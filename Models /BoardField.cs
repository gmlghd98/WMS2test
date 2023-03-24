using System;
using System.Collections.Generic;

namespace Models;

public partial class BoardField
{
    public string BoardFieldId { get; set; } = null!;

    public string? BoardId { get; set; }

    public string? BoardFieldType { get; set; }

    public string? BoardFieldName { get; set; }

    public string? RefBoardId { get; set; }

    public int? DisplayPosition { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Board? Board { get; set; }

    public virtual ICollection<BoardListValue> BoardListValues { get; } = new List<BoardListValue>();
}
