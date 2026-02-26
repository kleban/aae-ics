using AAEICS.Core.DTO.General;

namespace AAEICS.Core.DTO.Certificates;

public class IncomingCertificateDTO
{
    public int IncCertificateId { get; set; }
    
    public int Edrpou { get; set; }
    
    public PersonnelDTO ApprovePerson { get; set; }
    
    public DateTime? ApproveDate { get; set; }
    
    public DateTime RegistrationDate { get; set; }
    
    public string RegistrationPlace { get; set; } = null!;
    
    public DateTime TransferDateStart { get; set; }
    
    public DateTime TransferDateEnd { get; set; }
    
    public int DonorId { get; set; }
    
    public int RecipientId { get; set; }
    
    public int DeliveryCompany { get; set; }
    
    public int Reason { get; set; }
    
    public IEnumerable<IncomingCertificateLineDTO> CertificateLines { get; set; }
}