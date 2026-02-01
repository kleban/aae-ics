using System;
using System.Collections.Generic;

namespace AaeIcs.Database.Models;

public partial class TransferInstance
{
    public int InstanceId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<IncomingCardLine> IncomingCardLines { get; set; } = new List<IncomingCardLine>();

    public virtual ICollection<IncomingCertificate> IncomingCertificateDeliveryCompanyNavigations { get; set; } = new List<IncomingCertificate>();

    public virtual ICollection<IncomingCertificate> IncomingCertificateDonors { get; set; } = new List<IncomingCertificate>();

    public virtual ICollection<IncomingCertificate> IncomingCertificateRecipients { get; set; } = new List<IncomingCertificate>();

    public virtual ICollection<IssuanceCertificate> IssuanceCertificateDeliveryCompanyNavigations { get; set; } = new List<IssuanceCertificate>();

    public virtual ICollection<IssuanceCertificate> IssuanceCertificateDonors { get; set; } = new List<IssuanceCertificate>();

    public virtual ICollection<IssuanceCertificate> IssuanceCertificateRecipients { get; set; } = new List<IssuanceCertificate>();

    public virtual ICollection<IssueCardLine> IssueCardLineDonorNavigations { get; set; } = new List<IssueCardLine>();

    public virtual ICollection<IssueCardLine> IssueCardLineRecipientNavigations { get; set; } = new List<IssueCardLine>();

    public virtual ICollection<RecordCard> RecordCardDeliveryCentreNavigations { get; set; } = new List<RecordCard>();

    public virtual ICollection<RecordCard> RecordCardManagementCompanyNavigations { get; set; } = new List<RecordCard>();
}
