using Constellation.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.ToTable("Memberships");

        builder.HasKey(m => new { m.UserId, m.CompanyId });

        builder.Property(m => m.RoleId)
            .IsRequired();

        builder.HasIndex(m => m.RoleId);
    }
}