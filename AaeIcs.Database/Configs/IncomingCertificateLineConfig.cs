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
        entity.Property(e => e.CategoryReceivedId).HasColumnName("category_received_id");
        entity.Property(e => e.CategorySentId).HasColumnName("category_sent_id");
        entity.Property(e => e.CertificateId).HasColumnName("certificate_id");
        entity.Property(e => e.MadeIn)
            .HasColumnType("VARCHAR")
            .HasColumnName("made_in");
        entity.Property(e => e.MeasureUnitId).HasColumnName("measure_unit_id");
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

        entity.HasOne(d => d.MeasureUnit).WithMany(p => p.IncomingCertificateLines)
            .HasForeignKey(d => d.MeasureUnitId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(d => d.CategorySent)
            .WithMany()
            .HasForeignKey(d => d.CategorySentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne(d => d.CategoryReceived)
            .WithMany()
            .HasForeignKey(d => d.CategoryReceivedId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}