using System;
using System.Collections.Generic;
using System.Text;
using Constellation.Domain.Boards;
using Constellation.Domain.Companies;
using Constellation.Domain.Projects;
using Constellation.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Constellation.Infrastructure.Persistence;

public class ConstellationDbContext : DbContext
{
    public ConstellationDbContext(DbContextOptions<ConstellationDbContext> options
        ) : base(options)
    { }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<CompanyRole> Roles => Set<CompanyRole>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<User> Users => Set<User>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<Board> Boards => Set<Board>();
    public DbSet<BoardColumn> BoardsColumns => Set<BoardColumn>();

    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<TaskAssignment> TaskAssignments => Set<TaskAssignment>();
}

