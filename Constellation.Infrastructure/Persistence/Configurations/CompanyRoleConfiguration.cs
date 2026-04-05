using Constellation.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CompanyRoleConfiguration : IEntityTypeConfiguration<CompanyRole>
{
    public void Configure(EntityTypeBuilder<CompanyRole> builder)
    {
        builder.ToTable("CompanyRoles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.IsSystemRole)
            .IsRequired();

        builder.HasIndex(r => new { r.CompanyId, r.Name })
            .IsUnique();
    }
}
