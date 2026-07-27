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
    public TaskItem Task { get; private set; } = default!;

    private TaskComment() { }

    public TaskComment(TaskItem task, Guid userId, string content, Guid? fileRecordId = null)
    {
        Id = Guid.NewGuid();
        TenantId = task.TenantId;
        TaskId = task.Id;
        Task = task;
        UserId = userId;
        Content = content;
        FileRecordId = fileRecordId;
        CreatedAtUtc = DateTime.UtcNow;
    }
}

