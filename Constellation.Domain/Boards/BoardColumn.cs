using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Boards;

public class BoardColumn
{
    public Guid Id { get; private set; }
    public Guid BoardId { get; private set; }
    public string Name { get; private set; } = default!;
    public int Order { get; private set; }

    private BoardColumn() { }

    public BoardColumn(Guid boardId, string name, int order)
    {
        Id = Guid.NewGuid();
        BoardId = boardId;
        Name = name;
        Order = order;
    }
}

