using Constellation.Domain.Projects;
using Constellation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/companies/{companyId:guid}/Project-statuses")]
public class ProjectStatusesController : ControllerBase
{
    private readonly ConstellationDbContext _dbContext;
    public ProjectStatusesController(ConstellationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public record ProjectStatusDto(Guid Id, string Name, string ColorHex, bool IsClosedState, int Order);

    [HttpPost]
    public async Task<ActionResult<ProjectStatusDto>> Create([FromRoute] Guid companyId, [FromBody] CreateProjectStatusRequest request)
    {
        var projectStatus = new ProjectStatus(companyId, request.Name, request.ColorHex, request.IsClosedState, request.Order);
        _dbContext.ProjectStatuses.Add(projectStatus);
        await _dbContext.SaveChangesAsync();
        return Ok(new ProjectStatusDto(projectStatus.Id, projectStatus.Name, projectStatus.ColorHex, projectStatus.IsClosedState, projectStatus.Order));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectStatusDto>>> GetAll([FromRoute] Guid companyId)
    {
        var projectStatuses = await _dbContext.ProjectStatuses
            .Where(ps => ps.TenantId == companyId)
            .ToListAsync();

        var projectStatusDtos = projectStatuses.Select(ps => new ProjectStatusDto(ps.Id, ps.Name, ps.ColorHex, ps.IsClosedState, ps.Order));
        return Ok(projectStatusDtos);
    }
}

public record CreateProjectStatusRequest(string Name, string ColorHex, bool IsClosedState, int Order);