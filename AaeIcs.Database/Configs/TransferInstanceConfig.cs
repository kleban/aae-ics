using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAEICS.Database.Configs;

public class TransferInstanceConfig : IEntityTypeConfiguration<TransferInstance>
{
    public void Configure(EntityTypeBuilder<TransferInstance> entity)
    {
        entity.HasKey(e => e.InstanceId);

        entity.HasIndex(e => e.InstanceId, "IX_TransferInstances_instance_id").IsUnique();

        entity.Property(e => e.InstanceId)
            .ValueGeneratedNever()
            .HasColumnName("instance_id");
        entity.Property(e => e.Name)
            .HasColumnType("VARCHAR")
            .HasColumnName("name");
    }
}