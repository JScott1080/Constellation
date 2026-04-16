using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Boards;

public class Board : BaseEntity
{
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; } = default!;
    public int Order { get; private set; }

    // Required for Entity Framework
    private Board() { }

    public Board(Guid tenantId, Guid projectId, string name, int order)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        ProjectId = projectId;
        Name = name;
        Order = order;
    }
}