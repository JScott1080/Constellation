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

    public void AssignUser(Guid userId, bool isLead = false)
    {
        if (_assignments.Any(a => a.UserId == userId)) return;
        if (isLead && _assignments.Any(a => a.IsLead))
            throw new InvalidOperationException("This task already has a lead.");

        _assignments.Add(new TaskAssignment(this.TenantId, this.Id, userId, isLead));
        this.UpdatedAtUtc = DateTime.UtcNow;
    }

    public void AddComment(Guid userId, string content, Guid? fileRecordId = null)
    {
        var comment = new TaskComment(this.TenantId, this.Id, userId, content, fileRecordId);
        _comments.Add(comment);
        this.UpdatedAtUtc = DateTime.UtcNow;
    }
}


