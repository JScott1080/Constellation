namespace Constellation.Domain.Companies;

public class Company
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Slug { get; private set; } = default!; // e.g., "acme-construction"
    public DateTime CreatedAt { get; private set; }

    // --- Multi-Tenancy Routing Fields ---
    public bool IsDedicatedDatabase { get; private set; } // True = Split DB, False = Shared
    public string? ConnectionString { get; private set; } // Only filled if IsDedicatedDatabase is true
    public string? CustomDomain { get; private set; }    // e.g., "://acme.com"

    private Company() { }

    public Company(string name, string slug, bool isDedicated = false, string? connectionString = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Slug = slug.ToLower().Replace(" ", "-"); // Ensure URL-friendly
        CreatedAt = DateTime.UtcNow;
        IsDedicatedDatabase = isDedicated;
        ConnectionString = connectionString;
    }
}


