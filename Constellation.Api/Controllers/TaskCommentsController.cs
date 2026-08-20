using Constellation.Domain.Tasks;
using Constellation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/tasks/{taskId}/Comments")]
public class TaskCommentsController : ControllerBase
{
    private readonly ConstellationDbContext _dbContext;
    public TaskCommentsController(ConstellationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public record TaskCommentDto(Guid Id, Guid UserId, string Content, Guid? FileRecordId);

    [HttpPost]
    public async Task<ActionResult<TaskCommentDto>> AddComment([FromRoute] Guid taskId, [FromBody] AddCommentRequest request)
    {
        var task = await _dbContext.TaskItems.FindAsync(taskId);
        if (task == null) return NotFound();

        var comment = task.AddComment(request.UserId, request.Content, request.FileRecordId);
        _dbContext.TaskComments.Add(comment);
        await _dbContext.SaveChangesAsync();
        return Ok(new TaskCommentDto(comment.Id, comment.UserId, comment.Content, comment.FileRecordId));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskCommentDto>>> GetAll([FromRoute] Guid taskId)
    {
        var comments = await _dbContext.TaskComments
            .Where(c => c.TaskId == taskId)
            .ToListAsync();

        var commentDtos = comments.Select(c => new TaskCommentDto(c.Id, c.UserId, c.Content, c.FileRecordId));
        return Ok(commentDtos);
    }
}

public record AddCommentRequest(Guid UserId, string Content, Guid? FileRecordId);