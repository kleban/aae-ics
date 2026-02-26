using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAEICS.Database.Configs;

public class IncomingCertificateLineConfig : IEntityTypeConfiguration<IncomingCertificateLine>
{
    public void Configure(EntityTypeBuilder<IncomingCertificateLine> entity)
    {
        entity.HasKey(e => e.IncLineId);

        entity.HasIndex(e => e.IncLineId, "IX_IncomingCertificateLines_inc_line_id").IsUnique();

        entity.Property(e => e.IncLineId)
            .ValueGeneratedOnAdd()
            .HasColumnName("inc_line_id");
        entity.Property(e => e.BatchNumber)
            .HasColumnType("VARCHAR")
            .HasColumnName("batch_number");
        entity.Property(e => e.CategoryReceived)
            .HasColumnType("NUMERIC")
            .HasColumnName("category_received");
        entity.Property(e => e.CategorySent)
            .HasColumnType("NUMERIC")
            .HasColumnName("category_sent");
        entity.Property(e => e.CertificateId).HasColumnName("certificate_id");
        entity.Property(e => e.MadeIn)
            .HasColumnType("VARCHAR")
            .HasColumnName("made_in");
        entity.Property(e => e.MeasureUnit).HasColumnName("measure_unit");
        entity.Property(e => e.Name)
            .HasColumnType("VARCHAR")
            .HasColumnName("name");
        entity.Property(e => e.NomenclatureCode)
            .HasColumnType("VARCHAR")
            .HasColumnName("nomenclature_code");
        entity.Property(e => e.Notes)
            .HasColumnType("VARCHAR")
            .HasColumnName("notes");
        entity.Property(e => e.OrdinalNumber).HasColumnName("ordinal_number");
        entity.Property(e => e.PricePerUnit).HasColumnName("price_per_unit");
        entity.Property(e => e.QuantityReceived)
            .HasColumnType("NUMERIC")
            .HasColumnName("quantity_received");
        entity.Property(e => e.QuantitySent)
            .HasColumnType("NUMERIC")
            .HasColumnName("quantity_sent");

        entity.HasOne(d => d.Certificate).WithMany(p => p.IncomingCertificateLines)
            .HasForeignKey(d => d.CertificateId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(d => d.MeasureUnitNavigation).WithMany(p => p.IncomingCertificateLines)
            .HasForeignKey(d => d.MeasureUnit)
            .OnDelete(DeleteBehavior.Restrict);
    }
}