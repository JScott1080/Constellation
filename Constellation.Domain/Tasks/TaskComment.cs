using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Tasks;

public class TaskComment
{
    public Guid Id { get; private set; }
    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }
    public string Message { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }

    private TaskComment() { }

    public TaskComment(Guid taskId, Guid userId, string message)
    {
        Id = Guid.NewGuid();
        TaskId = taskId;
        UserId = userId;
        Message = message;
        CreatedAt = DateTime.UtcNow;
    }
}

