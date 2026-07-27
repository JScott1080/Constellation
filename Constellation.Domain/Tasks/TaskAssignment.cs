using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Tasks;

public class TaskAssignment : BaseEntity
{
    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime AssignedAtUtc { get; private set; }
    public bool IsLead { get; private set; }
    public TaskItem Task { get; private set; } = default!;
    private TaskAssignment() { }

    public TaskAssignment(TaskItem task, Guid userId, bool isLead = false)
    {
        Id = Guid.NewGuid();
        TenantId = task.TenantId;
        TaskId = task.Id;
        Task = task;
        UserId = userId;
        IsLead = isLead;
        AssignedAtUtc = DateTime.UtcNow;
    }
}

