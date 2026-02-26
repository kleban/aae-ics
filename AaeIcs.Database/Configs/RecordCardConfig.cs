using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAEICS.Database.Configs;

public class RecordCardConfig : IEntityTypeConfiguration<RecordCard>
{
    public void Configure(EntityTypeBuilder<RecordCard> entity)
    {
        entity.HasKey(e => e.CardId);

        entity.HasIndex(e => e.CardId, "IX_RecordCards_card_id").IsUnique();

        entity.Property(e => e.CardId)
            .ValueGeneratedOnAdd()
            .HasColumnName("card_id");
        entity.Property(e => e.ArmyBaseName)
            .HasColumnType("VARCHAR")
            .HasColumnName("army_base_name");
        entity.Property(e => e.Container).HasColumnName("container");
        entity.Property(e => e.DeliveryCentre).HasColumnName("delivery_centre");
        entity.Property(e => e.IsRestrictToDispatch)
            .HasColumnType("BOOLEAN")
            .HasColumnName("is_restrict_to_dispatch");
        entity.Property(e => e.ManagementCompany).HasColumnName("management_company");
        entity.Property(e => e.MaxAmount)
            .HasColumnType("NUMERIC")
            .HasColumnName("max_amount");
        entity.Property(e => e.MinAmount)
            .HasColumnType("NUMERIC")
            .HasColumnName("min_amount");
        entity.Property(e => e.MinisterialName)
            .HasColumnType("VARCHAR")
            .HasColumnName("ministerial_name");
        entity.Property(e => e.Rack).HasColumnName("rack");
        entity.Property(e => e.RegisterNumber).HasColumnName("register_number");
        entity.Property(e => e.ResponsiblePerson).HasColumnName("responsible_person");
        entity.Property(e => e.Shelf).HasColumnName("shelf");
        entity.Property(e => e.Storage).HasColumnName("storage");
        entity.Property(e => e.TransferLine).HasColumnName("transfer_line");

        // entity.HasOne(d => d.DeliveryCentreNavigation).WithMany(p => p.RecordCardDeliveryCentreNavigations)
        //     .HasForeignKey(d => d.DeliveryCentre);
        //
        // entity.HasOne(d => d.ManagementCompanyNavigation).WithMany(p => p.RecordCardManagementCompanyNavigations)
        //     .HasForeignKey(d => d.ManagementCompany);

        entity.HasOne(d => d.ResponsiblePersonNavigation).WithMany(p => p.RecordCards)
            .HasForeignKey(d => d.ResponsiblePerson)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.TransferLineNavigation).WithMany(p => p.RecordCards)
            .HasForeignKey(d => d.TransferLine)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}