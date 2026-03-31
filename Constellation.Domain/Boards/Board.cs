using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Boards;

public class Board
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; } = default!;
    public int Order { get; private set; }

    private Board() { }

    public Board(Guid projectId, string name, int order)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        Name = name;
        Order = order;
    }
}

