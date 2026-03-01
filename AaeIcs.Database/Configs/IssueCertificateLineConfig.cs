using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAEICS.Database.Configs;

public class IssueCertificateLineConfig : IEntityTypeConfiguration<IssueCertificateLine>
{
    public void Configure(EntityTypeBuilder<IssueCertificateLine> entity)
    {
        entity.HasKey(e => e.IssueLineId);

        entity.HasIndex(e => e.IssueLineId, "IX_IssueCertificateLines_issue_line_id").IsUnique();

        entity.Property(e => e.IssueLineId)
            .ValueGeneratedNever()
            .HasColumnName("issue_line_id");
        entity.Property(e => e.BatchNumber)
            .HasColumnType("VARCHAR")
            .HasColumnName("batch_number");
        entity.Property(e => e.CategoryReceivedId).HasColumnName("category_received_id");
        entity.Property(e => e.CategorySentId).HasColumnName("category_sent_id");
        entity.Property(e => e.CertificateId).HasColumnName("certificate_id");
        entity.Property(e => e.MeasureUnitId).HasColumnName("measure_unit_id");
        entity.Property(e => e.Name)
            .HasColumnType("VARCHAR")
            .HasColumnName("name");
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

        entity.HasOne(d => d.Certificate).WithMany(p => p.IssueCertificateLines)
            .HasForeignKey(d => d.CertificateId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.MeasureUnit).WithMany(p => p.IssueCertificateLines)
            .HasForeignKey(d => d.MeasureUnitId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.HasOne(d => d.CategorySent).WithMany()
            .HasForeignKey(d => d.CategorySentId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.HasOne(d => d.CategoryReceived).WithMany()
            .HasForeignKey(d => d.CategoryReceivedId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
