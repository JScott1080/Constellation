using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Companies;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!; // You'll need this!
    public DateTime CreatedDate { get; private set; }
    public DateTime? LastLoginUtc { get; private set; }

    private User() { }

    public User(string name, string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email.ToLower().Trim();
        PasswordHash = passwordHash;
        CreatedDate = DateTime.UtcNow;
    }
}

