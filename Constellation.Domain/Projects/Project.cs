using System;
using System.Collections.Generic;
using System.Text;
using Constellation.Domain.Boards;

namespace Constellation.Domain.Projects;

public class Project : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public Guid StatusId { get; private set; }
    public virtual ProjectStatus Status { get; private set; } = default!;
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public IReadOnlyCollection<Board> Boards => _boards.AsReadOnly();
    private readonly List<Board> _boards = new();

    private Project() { }

    public Project(Guid tenantId, string name, Guid initialStatusId, string? description = null)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        Name = name;
        Description = description;
        StatusId = initialStatusId; 
        CreatedAtUtc = DateTime.UtcNow;

        var board = AddBoard("Default Board", 0, false);
        board.AddColumn("Planning", 0, false);
        board.AddColumn("In Progress", 1, false);
        board.AddColumn("Inspection", 2, false);
        board.AddColumn("Completed", 3, false);
    }

    public void UpdateStatus(Guid newStatusId)
    {
        // Business Logic: You can now check if this status belongs to the same tenant
        StatusId = newStatusId;
    }

    public Board AddBoard(string name, int order, bool touchUpdatedAt = true)
    {
        var board = new Board(this, name, order);
        _boards.Add(board);
        if (touchUpdatedAt) UpdatedAtUtc = DateTime.UtcNow;
        return board;
    }
}
