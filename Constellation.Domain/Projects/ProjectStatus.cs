using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Projects;

public class ProjectStatus : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string ColorHex { get; private set; } = default!; // For the Web UI
    public bool IsClosedState { get; private set; } // Does this count as "Done"?
    public int Order { get; private set; }
    public bool IsSystemStatus { get; private set; }

    private ProjectStatus() { }

    public ProjectStatus(Guid tenantId, string name, string color, bool isClosed, int order, bool isSystem = false)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        Name = name;
        ColorHex = color;
        IsClosedState = isClosed;
        Order = order;
        IsSystemStatus = isSystem;
    }
}
