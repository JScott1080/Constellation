using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Companies;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public DateTime CreatedDate { get; private set; }

    private User()
    {
    }

    public User(string name, string email)
    {
        Id = Guid.NewGuid();
        Email = email;
        Name = name;
        CreatedDate = DateTime.UtcNow;
    }
}
