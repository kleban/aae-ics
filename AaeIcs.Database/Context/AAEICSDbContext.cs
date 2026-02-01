using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AaeIcs.Database.Models;

public partial class AAEICSDbContext : DbContext
{
    public AAEICSDbContext()
    {
    }

    public AAEICSDbContext(DbContextOptions<AAEICSDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentIncomingCardLine> DocumentIncomingCardLines { get; set; }

    public virtual DbSet<DocumentIssueCardLine> DocumentIssueCardLines { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<IncomingCardLine> IncomingCardLines { get; set; }

    public virtual DbSet<IncomingCertificate> IncomingCertificates { get; set; }

    public virtual DbSet<IncomingCertificateLine> IncomingCertificateLines { get; set; }

    public virtual DbSet<IssuanceCertificate> IssuanceCertificates { get; set; }

    public virtual DbSet<IssueCardLine> IssueCardLines { get; set; }

    public virtual DbSet<IssueCertificateLine> IssueCertificateLines { get; set; }

    public virtual DbSet<MeasureUnit> MeasureUnits { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<Reason> Reasons { get; set; }

    public virtual DbSet<RecordCard> RecordCards { get; set; }

    public virtual DbSet<TransferInstance> TransferInstances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=E:\\aae-ics\\DbIngeneering\\database.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Categories_id").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasIndex(e => e.DocumentId, "IX_Documents_document_id").IsUnique();

            entity.Property(e => e.DocumentId)
                .ValueGeneratedNever()
                .HasColumnName("document_id");
            entity.Property(e => e.Date)
                .HasColumnType("DATE")
                .HasColumnName("date");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR")
                .HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Documents).HasForeignKey(d => d.Type);
        });

        modelBuilder.Entity<DocumentIncomingCardLine>(entity =>
        {
            entity.HasKey(e => new { e.DocumentId, e.CardLineId });

            entity.ToTable("Document_IncomingCardLine");

            entity.HasIndex(e => e.DocumentId, "IX_Document_IncomingCardLine_document_id").IsUnique();

            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.CardLineId).HasColumnName("card_line_id");

            entity.HasOne(d => d.CardLine).WithMany(p => p.DocumentIncomingCardLines)
                .HasForeignKey(d => d.CardLineId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Document).WithOne(p => p.DocumentIncomingCardLine)
                .HasForeignKey<DocumentIncomingCardLine>(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DocumentIssueCardLine>(entity =>
        {
            entity.HasKey(e => new { e.DocumentId, e.CardLineId });

            entity.ToTable("Document_IssueCardLine");

            entity.HasIndex(e => e.DocumentId, "IX_Document_IssueCardLine_document_id").IsUnique();

            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.CardLineId).HasColumnName("card_line_id");

            entity.HasOne(d => d.CardLine).WithMany(p => p.DocumentIssueCardLines)
                .HasForeignKey(d => d.CardLineId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Document).WithOne(p => p.DocumentIssueCardLine)
                .HasForeignKey<DocumentIssueCardLine>(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.TypeId);

            entity.HasIndex(e => e.TypeId, "IX_DocumentTypes_type_id").IsUnique();

            entity.Property(e => e.TypeId)
                .ValueGeneratedNever()
                .HasColumnName("type_id");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR")
                .HasColumnName("name");
        });

        modelBuilder.Entity<IncomingCardLine>(entity =>
        {
            entity.HasKey(e => e.CardLineId);

            entity.HasIndex(e => e.CardLineId, "IX_IncomingCardLines_card_line_id").IsUnique();

            entity.Property(e => e.CardLineId)
                .ValueGeneratedNever()
                .HasColumnName("card_line_id");
            entity.Property(e => e.CarBrand)
                .HasColumnType("VARCHAR")
                .HasColumnName("car_brand");
            entity.Property(e => e.CarNumber)
                .HasColumnType("VARCHAR")
                .HasColumnName("car_number");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Donor).HasColumnName("donor");
            entity.Property(e => e.LineDate)
                .HasColumnType("DATE")
                .HasColumnName("line_date");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMERIC")
                .HasColumnName("quantity");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.ReferenceDocument).HasColumnName("reference_document");

            entity.HasOne(d => d.Card).WithMany(p => p.IncomingCardLines)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.IncomingCardLines)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DonorNavigation).WithMany(p => p.IncomingCardLines)
                .HasForeignKey(d => d.Donor)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ReasonNavigation).WithMany(p => p.IncomingCardLines)
                .HasForeignKey(d => d.Reason)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<IncomingCertificate>(entity =>
        {
            entity.HasKey(e => e.IncCertificateId);

            entity.HasIndex(e => e.IncCertificateId, "IX_IncomingCertificates_inc_certificate_id").IsUnique();

            entity.Property(e => e.IncCertificateId)
                .ValueGeneratedNever()
                .HasColumnName("inc_certificate_id");
            entity.Property(e => e.ApproveDate)
                .HasColumnType("DATE")
                .HasColumnName("approve_date");
            entity.Property(e => e.ApprovePerson).HasColumnName("approve_person");
            entity.Property(e => e.DeliveryCompany).HasColumnName("delivery_company");
            entity.Property(e => e.DonorId).HasColumnName("donor_id");
            entity.Property(e => e.Edrpou).HasColumnName("edrpou");
            entity.Property(e => e.Reason).HasColumnName("reason");
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

            entity.HasOne(d => d.ApprovePersonNavigation).WithMany(p => p.IncomingCertificates)
                .HasForeignKey(d => d.ApprovePerson)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DeliveryCompanyNavigation).WithMany(p => p.IncomingCertificateDeliveryCompanyNavigations)
                .HasForeignKey(d => d.DeliveryCompany)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Donor).WithMany(p => p.IncomingCertificateDonors)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ReasonNavigation).WithMany(p => p.IncomingCertificates)
                .HasForeignKey(d => d.Reason)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Recipient).WithMany(p => p.IncomingCertificateRecipients)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<IncomingCertificateLine>(entity =>
        {
            entity.HasKey(e => e.IncLineId);

            entity.HasIndex(e => e.IncLineId, "IX_IncomingCertificateLines_inc_line_id").IsUnique();

            entity.Property(e => e.IncLineId)
                .ValueGeneratedNever()
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
        });

        modelBuilder.Entity<IssuanceCertificate>(entity =>
        {
            entity.HasKey(e => e.IssueCertificateId);

            entity.HasIndex(e => e.IssueCertificateId, "IX_IssuanceCertificates_issue_certificate_id").IsUnique();

            entity.Property(e => e.IssueCertificateId)
                .ValueGeneratedNever()
                .HasColumnName("issue_certificate_id");
            entity.Property(e => e.ApproveDate)
                .HasColumnType("DATE")
                .HasColumnName("approve_date");
            entity.Property(e => e.ApprovePerson).HasColumnName("approve_person");
            entity.Property(e => e.DeliveryCompany).HasColumnName("delivery_company");
            entity.Property(e => e.DonorId).HasColumnName("donor_id");
            entity.Property(e => e.Edrpou).HasColumnName("edrpou");
            entity.Property(e => e.Reason).HasColumnName("reason");
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

            entity.HasOne(d => d.ApprovePersonNavigation).WithMany(p => p.IssuanceCertificates)
                .HasForeignKey(d => d.ApprovePerson)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DeliveryCompanyNavigation).WithMany(p => p.IssuanceCertificateDeliveryCompanyNavigations)
                .HasForeignKey(d => d.DeliveryCompany)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Donor).WithMany(p => p.IssuanceCertificateDonors)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ReasonNavigation).WithMany(p => p.IssuanceCertificates)
                .HasForeignKey(d => d.Reason)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Recipient).WithMany(p => p.IssuanceCertificateRecipients)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<IssueCardLine>(entity =>
        {
            entity.HasKey(e => e.CardLineId);

            entity.HasIndex(e => e.CardLineId, "IX_IssueCardLines_card_line_id").IsUnique();

            entity.Property(e => e.CardLineId)
                .ValueGeneratedNever()
                .HasColumnName("card_line_id");
            entity.Property(e => e.CarBrand)
                .HasColumnType("VARCHAR")
                .HasColumnName("car_brand");
            entity.Property(e => e.CarNumber)
                .HasColumnType("VARCHAR")
                .HasColumnName("car_number");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.CargoRecipientData)
                .HasColumnType("VARCHAR")
                .HasColumnName("cargo_recipient_data");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Donor).HasColumnName("donor");
            entity.Property(e => e.LineDate)
                .HasColumnType("DATE")
                .HasColumnName("line_date");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Recipient).HasColumnName("recipient");
            entity.Property(e => e.ReferenceDocument).HasColumnName("reference_document");

            entity.HasOne(d => d.Card).WithMany(p => p.IssueCardLines)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.IssueCardLines)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DonorNavigation).WithMany(p => p.IssueCardLineDonorNavigations)
                .HasForeignKey(d => d.Donor)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ReasonNavigation).WithMany(p => p.IssueCardLines)
                .HasForeignKey(d => d.Reason)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.RecipientNavigation).WithMany(p => p.IssueCardLineRecipientNavigations)
                .HasForeignKey(d => d.Recipient)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<IssueCertificateLine>(entity =>
        {
            entity.HasKey(e => e.IssueLineId);

            entity.HasIndex(e => e.IssueLineId, "IX_IssueCertificateLines_issue_line_id").IsUnique();

            entity.Property(e => e.IssueLineId)
                .ValueGeneratedNever()
                .HasColumnName("issue_line_id");
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
            entity.Property(e => e.MeasureUnit).HasColumnName("measure_unit");
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

            entity.HasOne(d => d.MeasureUnitNavigation).WithMany(p => p.IssueCertificateLines)
                .HasForeignKey(d => d.MeasureUnit)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<MeasureUnit>(entity =>
        {
            entity.HasKey(e => e.UnitId);

            entity.HasIndex(e => e.UnitId, "IX_MeasureUnits_unit_id").IsUnique();

            entity.Property(e => e.UnitId)
                .ValueGeneratedNever()
                .HasColumnName("unit_id");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Personnel>(entity =>
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
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasIndex(e => e.PositionId, "IX_Positions_position_id").IsUnique();

            entity.Property(e => e.PositionId)
                .ValueGeneratedNever()
                .HasColumnName("position_id");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Rank>(entity =>
        {
            entity.HasIndex(e => e.RankId, "IX_Ranks_rank_id").IsUnique();

            entity.Property(e => e.RankId)
                .ValueGeneratedNever()
                .HasColumnName("rank_id");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Reason>(entity =>
        {
            entity.HasIndex(e => e.ReasonId, "IX_Reasons_reason_id").IsUnique();

            entity.Property(e => e.ReasonId)
                .ValueGeneratedNever()
                .HasColumnName("reason_id");
            entity.Property(e => e.Date)
                .HasColumnType("DATE")
                .HasColumnName("date");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR")
                .HasColumnName("name");
        });

        modelBuilder.Entity<RecordCard>(entity =>
        {
            entity.HasKey(e => e.CardId);

            entity.HasIndex(e => e.CardId, "IX_RecordCards_card_id").IsUnique();

            entity.Property(e => e.CardId)
                .ValueGeneratedNever()
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

            entity.HasOne(d => d.DeliveryCentreNavigation).WithMany(p => p.RecordCardDeliveryCentreNavigations).HasForeignKey(d => d.DeliveryCentre);

            entity.HasOne(d => d.ManagementCompanyNavigation).WithMany(p => p.RecordCardManagementCompanyNavigations).HasForeignKey(d => d.ManagementCompany);

            entity.HasOne(d => d.ResponsiblePersonNavigation).WithMany(p => p.RecordCards)
                .HasForeignKey(d => d.ResponsiblePerson)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.TransferLineNavigation).WithMany(p => p.RecordCards)
                .HasForeignKey(d => d.TransferLine)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TransferInstance>(entity =>
        {
            entity.HasKey(e => e.InstanceId);

            entity.HasIndex(e => e.InstanceId, "IX_TransferInstances_instance_id").IsUnique();

            entity.Property(e => e.InstanceId)
                .ValueGeneratedNever()
                .HasColumnName("instance_id");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
