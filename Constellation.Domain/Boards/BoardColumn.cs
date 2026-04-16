using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Boards;

public class BoardColumn : BaseEntity
{
    public Guid BoardId { get; private set; }
    public string Name { get; private set; } = default!;
    public int Order { get; private set; }

    private BoardColumn() { }

    public BoardColumn(Guid tenantId, Guid boardId, string name, int order)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId; // Pass this down from the Board
        BoardId = boardId;
        Name = name;
        Order = order;
    }
}

