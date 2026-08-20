using System;
using System.Collections.Generic;
using System.Text;
using Constellation.Domain.Projects;

namespace Constellation.Domain.Boards;

public class Board : BaseEntity
{
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; } = default!;
    public int Order { get; private set; }
    public Project Project { get; private set; } = default!;
    private readonly List<BoardColumn> _columns = new();
    public IReadOnlyCollection<BoardColumn> Columns => _columns.AsReadOnly();

    // Required for Entity Framework
    private Board() { }

    public Board(Project project, string name, int order)
    {
        Id = Guid.NewGuid();
        TenantId = project.TenantId;
        ProjectId = project.Id;
        Project = project;
        Name = name;
        Order = order;
    }

    public BoardColumn AddColumn(string name, int order, bool touchUpdatedAt = true)
    {
        var column = new BoardColumn(this, name, order);
        _columns.Add(column);
        if (touchUpdatedAt) UpdatedAtUtc = DateTime.UtcNow;
        return column;
    }
}