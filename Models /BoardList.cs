using System;
using System.Collections.Generic;

namespace Models;

public partial class BoardList
{
    public string BoardListId { get; set; } = null!;

    public string? BoardId { get; set; }

    public string? GroupName { get; set; }

    public string? BoardListName { get; set; }

    public string? ParentBoardListId { get; set; }

    public virtual Board? Board { get; set; }

    public virtual ICollection<BoardListValue> BoardListValues { get; } = new List<BoardListValue>();
}
