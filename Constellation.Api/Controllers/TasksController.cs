using Constellation.Domain.Tasks;
using Constellation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/columns/{columnId:guid}/tasks")]
public class TasksController : ControllerBase
{
    private readonly ConstellationDbContext _dbContext;
    public TasksController(ConstellationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public record TaskDto(Guid Id, string Title, string? Description, int Order, Guid ColumnId);

    [HttpPost]
    public async Task<ActionResult<TaskDto>> Create([FromRoute] Guid columnId, [FromBody] CreateTaskRequest request)
    {
        var column = await _dbContext.BoardsColumns.FindAsync(columnId);
        if (column == null) return NotFound();

        var order = await _dbContext.TaskItems.CountAsync(t => t.BoardColumnId == columnId);
        var task = new TaskItem(column.TenantId, columnId, request.Title, order, request.Description);
        
        _dbContext.TaskItems.Add(task);
        
        await _dbContext.SaveChangesAsync();
        return Ok(new TaskDto(task.Id, task.Title, task.Description, task.Order, task.BoardColumnId));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll([FromRoute] Guid columnId)
    {
        var tasks = await _dbContext.TaskItems
            .Where(t => t.BoardColumnId == columnId)
            .ToListAsync();

        var taskDtos = tasks.Select(t => new TaskDto(t.Id, t.Title, t.Description, t.Order, t.BoardColumnId));
        return Ok(taskDtos);
    }
}

public record CreateTaskRequest(string Title, string? Description);