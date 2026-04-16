using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Projects;

public class Project : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public Guid StatusId { get; private set; }
    public virtual ProjectStatus Status { get; private set; } = default!;
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    private Project() { }

    public Project(Guid tenantId, string name, Guid initialStatusId, string? description = null)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        Name = name;
        Description = description;
        StatusId = initialStatusId; 
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void UpdateStatus(Guid newStatusId)
    {
        // Business Logic: You can now check if this status belongs to the same tenant
        StatusId = newStatusId;
    }
}
