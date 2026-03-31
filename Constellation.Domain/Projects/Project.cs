using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Projects;

public class Project
{
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public DateTime CreatedDate { get; private set; }

    private Project() { }

    public Project(Guid id, Guid companyId, string name, string? description = null)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Description = description;
        CreatedDate = DateTime.UtcNow;
    }
}
