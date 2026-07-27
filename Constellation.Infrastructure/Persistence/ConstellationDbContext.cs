using System;
using System.Collections.Generic;
using System.Text;
using Constellation.Domain.Boards;
using Constellation.Domain.Companies;
using Constellation.Domain.Files;
using Constellation.Domain.Projects;
using Constellation.Domain.Tasks;
using Microsoft.EntityFrameworkCore;

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
    public DbSet<ProjectStatus> ProjectStatuses => Set<ProjectStatus>();

    public DbSet<Board> Boards => Set<Board>();
    public DbSet<BoardColumn> BoardsColumns => Set<BoardColumn>();

    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<TaskAssignment> TaskAssignments => Set<TaskAssignment>();
    public DbSet<TaskComment> TaskComments => Set<TaskComment>();

    public DbSet<FileRecord> Files => Set<FileRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConstellationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

