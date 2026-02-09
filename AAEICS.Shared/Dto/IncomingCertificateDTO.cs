namespace AAEICS.Shared.Dto;

public class IncomingCertificateDto
{
    public int IncCertificateId { get; set; }

    public int Edrpou { get; set; }

    public string ApprovePerson { get; set; }

    public DateTime? ApproveDate { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string RegistrationPlace { get; set; } = null!;

    public DateTime TransferDateStart { get; set; }

    public DateTime TransferDateEnd { get; set; }

    public string Donor { get; set; }

    public string Recipient { get; set; }

    public string Deliver { get; set; }

    public string Reason { get; set; }
}