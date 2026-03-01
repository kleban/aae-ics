using System;
using System.Collections.Generic;

namespace AAEICS.Database.Models;

public partial class IssuanceCertificate
{
    public int IssueCertificateId { get; set; }

    public int Edrpou { get; set; }

    public int ApprovePersonId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string RegistrationPlace { get; set; } = null!;

    public DateTime TransferEndDate { get; set; }

    public int DonorId { get; set; }

    public int RecipientId { get; set; }

    public int DeliveryCompanyId { get; set; }

    public int ReasonId { get; set; }

    public virtual Personnel ApprovePerson { get; set; } = null!;

    public virtual TransferInstance DeliveryCompany { get; set; } = null!;

    public virtual TransferInstance Donor { get; set; } = null!;

    public virtual ICollection<IssueCertificateLine> IssueCertificateLines { get; set; } = new List<IssueCertificateLine>();

    public virtual Reason Reason { get; set; } = null!;

    public virtual TransferInstance Recipient { get; set; } = null!;
}
