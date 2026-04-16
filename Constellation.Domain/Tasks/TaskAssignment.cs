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

    private TaskAssignment() { }

    public TaskAssignment(Guid tenantId, Guid taskId, Guid userId, bool isLead = false)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId; 
        TaskId = taskId;
        UserId = userId;
        IsLead = isLead;
        AssignedAtUtc = DateTime.UtcNow;
    }
}

