using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Tasks;

public class TaskItem : BaseEntity
{
    public Guid BoardColumnId { get; private set; }
    public string Title { get; private set; } = default!;
    public string? Description { get; private set; }
    public int Order { get; private set; }

    private readonly List<TaskAssignment> _assignments = new();
    public IReadOnlyCollection<TaskAssignment> Assignments => _assignments.AsReadOnly();

    private readonly List<TaskComment> _comments = new();
    public IReadOnlyCollection<TaskComment> Comments => _comments.AsReadOnly();

    private TaskItem() { }

    public TaskItem(Guid tenantId, Guid boardColumnId, string title, int order, string? description = null)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        BoardColumnId = boardColumnId;
        Title = title;
        Order = order;
        Description = description;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public TaskAssignment AssignUser(Guid userId, bool isLead = false)
    {
        var existing = _assignments.FirstOrDefault(a => a.UserId == userId);
        if (existing != null) return existing;

        if (isLead && _assignments.Any(a => a.IsLead))
            throw new InvalidOperationException("This task already has a lead.");

        var assignment = new TaskAssignment(this, userId, isLead);
        _assignments.Add(assignment);
        this.UpdatedAtUtc = DateTime.UtcNow;
        return assignment;
    }

    public TaskComment AddComment(Guid userId, string content, Guid? fileRecordId = null)
    {
        var comment = new TaskComment(this, userId, content, fileRecordId);
        _comments.Add(comment);
        this.UpdatedAtUtc = DateTime.UtcNow;
        return comment;
    }
}


