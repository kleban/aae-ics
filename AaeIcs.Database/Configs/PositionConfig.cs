using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAEICS.Database.Configs;

public class PositionConfig : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> entity)
    {
        entity.HasIndex(e => e.PositionId, "IX_Positions_position_id").IsUnique();

        entity.Property(e => e.PositionId)
            .ValueGeneratedNever()
            .HasColumnName("position_id");
        entity.Property(e => e.Name)
            .HasColumnType("VARCHAR")
            .HasColumnName("name");
    }
}