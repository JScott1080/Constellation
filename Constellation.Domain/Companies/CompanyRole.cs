using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Companies;

public class CompanyRole
{
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; } = default!;
    public bool IsSystemRole { get; private set; }

    private CompanyRole() { }

    public CompanyRole(Guid companyId, string name, bool isSystemRole = false)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        Name = name;
        IsSystemRole = isSystemRole;
    }
}
