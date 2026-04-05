using Constellation.Domain.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

public class FileRecordConfiguration : IEntityTypeConfiguration<FileRecord>
{
    public void Configure(EntityTypeBuilder<FileRecord> builder)
    {
        builder.ToTable("Files");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.FileName)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(f => f.StoragePath)
            .IsRequired()
            .HasMaxLength (1024);

        builder.Property(f => f.Size)
            .IsRequired();

        builder.Property(f => f.UploadedAt)
            .IsRequired ();

        builder.HasIndex(f => f.CompanyId);
    }
}