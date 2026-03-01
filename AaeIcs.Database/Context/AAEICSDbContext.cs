using AAEICS.Database.Database;
using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AAEICS.Database.Context;

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
    
    // public virtual DbSet<Document> Documents { get; set; }
    
    // public virtual DbSet<DocumentIncomingCardLine> DocumentIncomingCardLines { get; set; }
    //
    // public virtual DbSet<DocumentIssueCardLine> DocumentIssueCardLines { get; set; }
    //
    // public virtual DbSet<DocumentType> DocumentTypes { get; set; }
    
    // public virtual DbSet<IncomingCardLine> IncomingCardLines { get; set; }

    public virtual DbSet<IncomingCertificate> IncomingCertificates { get; set; }

    public virtual DbSet<IncomingCertificateLine> IncomingCertificateLines { get; set; }

    public virtual DbSet<IssuanceCertificate> IssuanceCertificates { get; set; }
    
    // public virtual DbSet<IssueCardLine> IssueCardLines { get; set; }
    
    public virtual DbSet<IssueCertificateLine> IssueCertificateLines { get; set; }

    public virtual DbSet<MeasureUnit> MeasureUnits { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<Reason> Reasons { get; set; }

    // public virtual DbSet<RecordCard> RecordCards { get; set; }

    public virtual DbSet<TransferInstance> TransferInstances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AAEICSDbContext).Assembly);
        modelBuilder.Seed();
    }
}
