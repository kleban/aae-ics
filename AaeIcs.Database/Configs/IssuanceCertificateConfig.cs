using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAEICS.Database.Configs;

public class IssuanceCertificateConfig : IEntityTypeConfiguration<IssuanceCertificate>
{
    public void Configure(EntityTypeBuilder<IssuanceCertificate> entity)
    {
        entity.HasKey(e => e.IssueCertificateId);

        entity.HasIndex(e => e.IssueCertificateId, "IX_IssuanceCertificates_issue_certificate_id").IsUnique();

        entity.Property(e => e.IssueCertificateId)
            .ValueGeneratedNever()
            .HasColumnName("issue_certificate_id");
        entity.Property(e => e.ApproveDate)
            .HasColumnType("DATE")
            .HasColumnName("approve_date");
        entity.Property(e => e.ApprovePersonId).HasColumnName("approve_person_id");
        entity.Property(e => e.DeliveryCompanyId).HasColumnName("delivery_company_id");
        entity.Property(e => e.DonorId).HasColumnName("donor_id");
        entity.Property(e => e.Edrpou).HasColumnName("edrpou");
        entity.Property(e => e.ReasonId).HasColumnName("reason_id");
        entity.Property(e => e.RecipientId).HasColumnName("recipient_id");
        entity.Property(e => e.RegistrationDate)
            .HasColumnType("DATE")
            .HasColumnName("registration_date");
        entity.Property(e => e.RegistrationPlace)
            .HasColumnType("VARCHAR")
            .HasColumnName("registration_place");
        entity.Property(e => e.TransferEndDate)
            .HasColumnType("DATE")
            .HasColumnName("transfer_end_date");

        entity.HasOne(d => d.ApprovePerson).WithMany(p => p.IssuanceCertificates)
            .HasForeignKey(d => d.ApprovePersonId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.DeliveryCompany).WithMany(p => p.IssuanceCertificateDeliveryCompanies)
            .HasForeignKey(d => d.DeliveryCompanyId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Donor).WithMany(p => p.IssuanceCertificateDonors)
            .HasForeignKey(d => d.DonorId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Reason).WithMany(p => p.IssuanceCertificates)
            .HasForeignKey(d => d.ReasonId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Recipient).WithMany(p => p.IssuanceCertificateRecipients)
            .HasForeignKey(d => d.RecipientId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
