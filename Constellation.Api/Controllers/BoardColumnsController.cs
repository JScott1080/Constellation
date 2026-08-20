using Constellation.Domain.Boards;
using Constellation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/boards/{boardId:guid}/columns")]
public class BoardColumnsController : ControllerBase
{
    private readonly ConstellationDbContext _dbContext;
    public BoardColumnsController(ConstellationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public record ColumnDto(Guid Id, string Name, int Order);

    [HttpPost]
    public async Task<ActionResult<ColumnDto>> Create([FromRoute] Guid boardId, [FromBody] CreateColumnRequest request)
    {
        var board = await _dbContext.Boards
            .Include(b => b.Columns)
            .FirstOrDefaultAsync(b => b.Id == boardId);


        if (board == null) return NotFound();

        var column = board.AddColumn(request.Name, board.Columns.Count);

        _dbContext.BoardsColumns.Add(column);
        await _dbContext.SaveChangesAsync();
        return Ok(new ColumnDto(column.Id, column.Name, column.Order));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ColumnDto>>> GetAll([FromRoute] Guid boardId, [FromQuery] Guid projectId)
    {
        var columns = await _dbContext.BoardsColumns
            .Where(c => c.BoardId == boardId)
            .ToListAsync();

        var columnDtos = columns.Select(c => new ColumnDto(c.Id, c.Name, c.Order));
        return Ok(columnDtos);
    }
}

public record CreateColumnRequest(string Name);