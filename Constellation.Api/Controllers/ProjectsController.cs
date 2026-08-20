using Constellation.Domain.Projects;
using Constellation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/companies/{companyId:guid}/projects")]
public class ProjectsController : ControllerBase
{
    private readonly ConstellationDbContext _dbContext;
    public ProjectsController(ConstellationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public record ProjectDto(Guid Id, string Name, string? Description, Guid StatusId, Guid CompanyId);

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> Create([FromRoute] Guid companyId, [FromBody] CreateProjectRequest request)
    {
        var project = new Project(companyId, request.Name, request.StatusId, request.Description);
        _dbContext.Projects.Add(project);
        await _dbContext.SaveChangesAsync();
        return Ok(new ProjectDto(project.Id, project.Name, project.Description ?? "", project.StatusId, project.TenantId));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll([FromRoute] Guid companyId)
    {
        var projects = await _dbContext.Projects
            .Where(p => p.TenantId == companyId)
            .ToListAsync();

        var projectDtos = projects.Select(p => new ProjectDto(p.Id, p.Name, p.Description ?? "", p.StatusId, p.TenantId));
        return Ok(projectDtos);
    }
}

public record CreateProjectRequest(string Name, string Description, Guid StatusId);