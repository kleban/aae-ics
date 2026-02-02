using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAEICS.Database.Configs;

public class RankConfig : IEntityTypeConfiguration<Rank>
{
    public void Configure(EntityTypeBuilder<Rank> entity)
    {
        entity.HasIndex(e => e.RankId, "IX_Ranks_rank_id").IsUnique();

        entity.Property(e => e.RankId)
            .ValueGeneratedNever()
            .HasColumnName("rank_id");
        entity.Property(e => e.Name)
            .HasColumnType("VARCHAR")
            .HasColumnName("name");
    }
}