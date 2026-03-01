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
    
    public TransferInstanceDTO Donor { get; set; }
    
    public TransferInstanceDTO Recipient { get; set; }
    
    public TransferInstanceDTO DeliveryCompany { get; set; }
    
    public ReasonDTO Reason { get; set; }
    
    public IEnumerable<IncomingCertificateLineDTO> IncomingCertificateLines { get; set; }
}
