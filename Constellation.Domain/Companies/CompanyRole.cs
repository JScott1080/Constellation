using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Companies;

public class CompanyRole : BaseEntity
{
    public string Name { get; private set; } = default!;
    public bool IsSystemRole { get; private set; }

    private readonly List<string> _permissions = new();
    public IReadOnlyCollection<string> Permissions => _permissions.AsReadOnly();

    private CompanyRole() { }

    public CompanyRole(Guid tenantId, string name, bool isSystemRole = false)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId; // From BaseEntity
        Name = name;
        IsSystemRole = isSystemRole;
    }

    public void AddPermission(string permission) { /* logic */ }
}
