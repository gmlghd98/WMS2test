using System;
using System.Collections.Generic;

namespace Models;

public partial class Board
{
    public string BoardId { get; set; } = null!;

    public string? BoardName { get; set; }

    public string? BoardTypeId { get; set; }

    public string? ParentBoardId { get; set; }

    public virtual ICollection<BoardField> BoardFields { get; } = new List<BoardField>();

    public virtual ICollection<BoardList> BoardLists { get; } = new List<BoardList>();

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
