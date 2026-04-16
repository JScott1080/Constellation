using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Companies;

public class Membership : BaseEntity 
{
    public Guid UserId { get; private set; }
    public Guid CompanyId { get; private set; } 
    public Guid RoleId { get; private set; }
    public DateTime JoinedAtUtc { get; private set; }

    private Membership() { }

    public Membership(Guid tenantId, Guid userId, Guid roleId)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        CompanyId = tenantId;
        RoleId = roleId;
        JoinedAtUtc = DateTime.UtcNow;
    }
}
