using Constellation.Domain.Tasks;
using Constellation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/tasks/{taskId}/assignments")]
public class TaskAssignmentsController : ControllerBase
{
    private readonly ConstellationDbContext _dbContext;
    public TaskAssignmentsController(ConstellationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public record TaskAssignmentDto(Guid Id, Guid UserId, bool IsLead);

    [HttpPost]
    public async Task<ActionResult<TaskAssignmentDto>> AssignUser([FromRoute] Guid taskId, [FromBody] AssignUserRequest request)
    {
        var task = await _dbContext.TaskItems
        .Include(t => t.Assignments)
        .FirstOrDefaultAsync(t => t.Id == taskId);
        
        if (task == null) return NotFound();

        try
        {
            var assignment = task.AssignUser(request.UserId, request.IsLead);
            await _dbContext.SaveChangesAsync();
            return Ok(new TaskAssignmentDto(assignment.Id, assignment.UserId, assignment.IsLead));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskAssignmentDto>>> GetAll([FromRoute] Guid taskId)
    {
        var assignments = await _dbContext.TaskAssignments
            .Where(a => a.TaskId == taskId)
            .ToListAsync();

        var assignmentDtos = assignments.Select(a => new TaskAssignmentDto(a.Id, a.UserId, a.IsLead));
        return Ok(assignmentDtos);
    }
}

public record AssignUserRequest(Guid UserId, bool IsLead);