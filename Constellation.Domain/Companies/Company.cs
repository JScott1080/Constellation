namespace Constellation.Domain.Companies;

public class Company
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }

    private Company() { } // EF Core

    public Company(string name, string slug)
    {
        Id = Guid.NewGuid();
        Name = name;
        Slug = slug;
        CreatedAt = DateTime.UtcNow;
    }
}

