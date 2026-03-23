using System;
using System.Collections.Generic;
using System.Text;
using Constellation.Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace Constellation.Infrastructure.Persistence;

public class ConstellationDbContext : DbContext
{
    public ConstellationDbContext(DbContextOptions<ConstellationDbContext> options
        ) : base(options)
    { }

    public DbSet<Company> Companies => Set<Company>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConstellationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

