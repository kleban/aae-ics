using System;
using System.Collections.Generic;

namespace AAEICS.Database.Models;

public partial class IssuanceCertificate
{
    public int IssueCertificateId { get; set; }

    public int Edrpou { get; set; }

    public int ApprovePerson { get; set; }

    public DateTime? ApproveDate { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string RegistrationPlace { get; set; } = null!;

    public DateTime TransferEndDate { get; set; }

    public int DonorId { get; set; }

    public int RecipientId { get; set; }

    public int DeliveryCompany { get; set; }

    public int Reason { get; set; }

    public virtual Personnel ApprovePersonNavigation { get; set; } = null!;

    public virtual TransferInstance DeliveryCompanyNavigation { get; set; } = null!;

    public virtual TransferInstance Donor { get; set; } = null!;

    public virtual ICollection<IssueCertificateLine> IssueCertificateLines { get; set; } = new List<IssueCertificateLine>();

    public virtual Reason ReasonNavigation { get; set; } = null!;

    public virtual TransferInstance Recipient { get; set; } = null!;
}
