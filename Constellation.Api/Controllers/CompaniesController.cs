using Constellation.Domain.Companies;
using Constellation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ConstellationDbContext _dbContext;
    public CompaniesController(ConstellationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public record CompanyDto(Guid Id, string Name, string Slug);

    [HttpPost]
    public async Task<ActionResult<CompanyDto>> Create([FromBody] CreateCompanyRequest request)
    {
        var company = new Company(request.Name, request.Slug);
        _dbContext.Companies.Add(company);
        await _dbContext.SaveChangesAsync();
        return Ok(new CompanyDto(company.Id, company.Name, company.Slug));
    }

    [HttpGet("{companyId:guid}")]
    public async Task<ActionResult<CompanyDto>> GetById([FromRoute] GetCompanyByIdRequest request)
    {
        var company = await _dbContext.Companies.FindAsync(request.CompanyId);
        if (company == null)
        {
            return NotFound();
        }
        return Ok(new CompanyDto(company.Id, company.Name, company.Slug));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll([FromQuery] GetAllCompaniesRequest request)
    {
        var companies = await _dbContext.Companies.ToListAsync();
        var companyDtos = companies.Select(c => new CompanyDto(c.Id, c.Name, c.Slug));
        return Ok(companyDtos);
    }

} 

public record CreateCompanyRequest(string Name, string Slug);
public record GetCompanyByIdRequest(Guid CompanyId);
public record GetAllCompaniesRequest();