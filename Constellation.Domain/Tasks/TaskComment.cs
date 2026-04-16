using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Tasks;

public class TaskComment : BaseEntity
{
    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }
    public string Content { get; private set; } = default!;
    public Guid? FileRecordId { get; private set; }

    private TaskComment() { }

    public TaskComment(Guid tenantId, Guid taskId, Guid userId, string content, Guid? fileRecordId = null)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        TaskId = taskId;
        UserId = userId;
        Content = content;
        FileRecordId = fileRecordId;
        CreatedAtUtc = DateTime.UtcNow;
    }
}

