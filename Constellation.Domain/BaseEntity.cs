using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public Guid TenantId { get; protected set; } // The "Brain" needs this for routing
    public DateTime CreatedAtUtc { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; protected set; }
}

