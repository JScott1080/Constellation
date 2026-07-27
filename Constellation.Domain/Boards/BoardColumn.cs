using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Boards;

public class BoardColumn : BaseEntity
{
    public Guid BoardId { get; private set; }
    public string Name { get; private set; } = default!;
    public int Order { get; private set; }
    public Board Board { get; private set; } = default!;

    private BoardColumn() { }

   public BoardColumn(Board board, string name, int order)
    {
        Id = Guid.NewGuid();
        TenantId = board.TenantId;
        BoardId = board.Id;
        Board = board;
        Name = name;
        Order = order;
    }
}

