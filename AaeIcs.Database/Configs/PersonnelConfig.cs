using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAEICS.Database.Configs;

public class PersonnelConfig : IEntityTypeConfiguration<Personnel>
{
    public void Configure(EntityTypeBuilder<Personnel> entity)
    {
        entity.HasKey(e => e.PersonId);

        entity.HasIndex(e => e.PersonId, "IX_Personnel_person_id").IsUnique();

        entity.Property(e => e.PersonId)
            .ValueGeneratedNever()
            .HasColumnName("person_id");
        entity.Property(e => e.FirstName)
            .HasColumnType("VARCHAR")
            .HasColumnName("first_name");
        entity.Property(e => e.LastName)
            .HasColumnType("VARCHAR")
            .HasColumnName("last_name");
        entity.Property(e => e.MiddleName)
            .HasColumnType("VARCHAR")
            .HasColumnName("middle_name");
        entity.Property(e => e.Position).HasColumnName("position");
        entity.Property(e => e.Rank).HasColumnName("rank");

        entity.HasOne(d => d.PositionNavigation).WithMany(p => p.Personnel).HasForeignKey(d => d.Position);

        entity.HasOne(d => d.RankNavigation).WithMany(p => p.Personnel).HasForeignKey(d => d.Rank);
    }
}