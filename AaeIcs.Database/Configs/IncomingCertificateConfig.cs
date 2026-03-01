using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAEICS.Database.Configs;

public class IncomingCertificateConfig : IEntityTypeConfiguration<IncomingCertificate>
{
    public void Configure(EntityTypeBuilder<IncomingCertificate> entity)
    {
        entity.HasKey(e => e.IncCertificateId);

        entity.HasIndex(e => e.IncCertificateId, "IX_IncomingCertificates_inc_certificate_id").IsUnique();

        entity.Property(e => e.IncCertificateId)
            .ValueGeneratedOnAdd()
            .HasColumnName("inc_certificate_id");
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
        entity.Property(e => e.TransferDateEnd)
            .HasColumnType("DATE")
            .HasColumnName("transfer_date_end");
        entity.Property(e => e.TransferDateStart)
            .HasColumnType("DATE")
            .HasColumnName("transfer_date_start");

        entity.HasOne(d => d.ApprovePerson).WithMany(p => p.IncomingCertificates)
            .HasForeignKey(d => d.ApprovePersonId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.DeliveryCompany).WithMany(p => p.IncomingCertificateDeliveryCompanies)
            .HasForeignKey(d => d.DeliveryCompanyId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.HasOne(d => d.Donor).WithMany(p => p.IncomingCertificateDonors)
            .HasForeignKey(d => d.DonorId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Reason).WithMany(p => p.IncomingCertificates)
            .HasForeignKey(d => d.ReasonId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Recipient).WithMany(p => p.IncomingCertificateRecipients)
            .HasForeignKey(d => d.RecipientId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}