using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Companies;

public class Membership
{
    public Guid UserId { get; private set; }
    public Guid CompanyId { get; private set; }
    public Guid RoleId { get; private set; }

    private Membership() { }

    public Membership(Guid userId, Guid companyId, Guid roleId)
    {
        UserId = userId;
        CompanyId = companyId;
        RoleId = roleId;
    }
}
