using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Tasks;

public class TaskAssignment
{
    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }

    private TaskAssignment() { }

    public TaskAssignment(Guid taskId, Guid userId)
    {
        TaskId = taskId;
        UserId = userId;
    }
}
