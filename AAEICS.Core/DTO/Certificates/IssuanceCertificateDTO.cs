using AAEICS.Core.DTO.General;

namespace AAEICS.Core.DTO.Certificates;

public class IssuanceCertificateDTO
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

    public PersonnelDTO ApprovePerson { get; set; } = null!;

    public virtual TransferInstanceDTO DeliveryCompany { get; set; } = null!;

    public virtual TransferInstanceDTO Donor { get; set; } = null!;

    public virtual ICollection<IssueCertificateLineDTO> IssueCertificateLines { get; set; }

    public virtual ReasonDTO Reason { get; set; } = null!;

    public virtual TransferInstanceDTO Recipient { get; set; } = null!;
}