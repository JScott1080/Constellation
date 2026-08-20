using Constellation.Domain.Boards;
using Constellation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using static BoardColumnsController;

[ApiController]
[Route("api/projects/{projectId:guid}/boards")]
public class BoardsController : ControllerBase
{
    private readonly ConstellationDbContext _dbContext;
    public BoardsController(ConstellationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public record BoardDto(Guid Id, string Name, Guid ProjectId, IReadOnlyCollection<ColumnDto> Columns);

    [HttpPost]
    public async Task<ActionResult<BoardDto>> Create([FromRoute] Guid projectId, [FromBody] CreateBoardRequest request)
    {
        var project = await _dbContext.Projects
            .Include(p => p.Boards)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null) return NotFound();
    
        var board = project.AddBoard(request.Name, project.Boards.Count);
        _dbContext.Boards.Add(board);
        await _dbContext.SaveChangesAsync();
        return Ok(new BoardDto(board.Id, board.Name, board.ProjectId, board.Columns.Select(c => new ColumnDto(c.Id, c.Name, c.Order)).ToList()));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetAll([FromRoute] Guid projectId)
    {
        var boards = await _dbContext.Boards
            .Include(b => b.Columns)
            .Where(b => b.ProjectId == projectId)
            .ToListAsync();

        var boardDtos = boards.Select(b => new BoardDto(b.Id, b.Name, b.ProjectId, b.Columns.Select(c => new ColumnDto(c.Id, c.Name, c.Order)).ToList()));
        return Ok(boardDtos);
    }
}

public record CreateBoardRequest(string Name);
