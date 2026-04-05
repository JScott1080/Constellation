using Constellation.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
{
    public void Configure(EntityTypeBuilder<TaskComment> builder)
    {
        builder.ToTable("TaskComments");

        builder.HasKey(t => t.Id);

        builder.Property(c => c.Message)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.HasIndex(c => c.TaskId);
    }
}