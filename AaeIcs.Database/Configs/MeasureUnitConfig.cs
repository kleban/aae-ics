using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAEICS.Database.Configs;

public class MeasureUnitConfig : IEntityTypeConfiguration<MeasureUnit>
{
    public void Configure(EntityTypeBuilder<MeasureUnit> entity)
    {
        entity.HasKey(e => e.UnitId);

        entity.HasIndex(e => e.UnitId, "IX_MeasureUnits_unit_id").IsUnique();

        entity.Property(e => e.UnitId)
            .ValueGeneratedNever()
            .HasColumnName("unit_id");
        entity.Property(e => e.Name)
            .HasColumnType("VARCHAR")
            .HasColumnName("name");
    }
}