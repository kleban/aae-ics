using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAEICS.Database.Configs;

public class ReasonConfig : IEntityTypeConfiguration<Reason>
{
    public void Configure(EntityTypeBuilder<Reason> entity)
    {
        entity.HasIndex(e => e.ReasonId, "IX_Reasons_reason_id").IsUnique();

        entity.Property(e => e.ReasonId)
            .ValueGeneratedNever()
            .HasColumnName("reason_id");
        entity.Property(e => e.Date)
            .HasColumnType("DATE")
            .HasColumnName("date");
        entity.Property(e => e.Name)
            .HasColumnType("VARCHAR")
            .HasColumnName("name");
    }
}