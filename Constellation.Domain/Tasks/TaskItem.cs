using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Tasks;

public class TaskItem
{
    public Guid Id { get; private set; }
    public Guid BoardColumnId { get; private set; }
    public string Title { get; private set; } = default!;
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private TaskItem() { }

    public TaskItem(Guid boardColumnId, string title, string? description = null)
    {
        Id = Guid.NewGuid();
        BoardColumnId = boardColumnId;
        Title = title;
        Description = description;
        CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    public void Update(string title, string? description)
    {
        Title = title;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}

