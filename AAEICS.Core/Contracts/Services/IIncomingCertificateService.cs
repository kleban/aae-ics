using AAEICS.Core.DTO.Certificates;

namespace AAEICS.Core.Contracts.Services;

public interface IIncomingCertificateService
{
    Task<IEnumerable<IncomingCertificateDTO>> GetIncomingCertificates();
    
    IncomingCertificateDTO GetIncomingCertificate(int id);
    
    Task<bool> AddIncomingCertificateAsync(IncomingCertificateDTO incomingCertificate, IEnumerable<IncomingCertificateLineDTO> incomingCertificateLines);
}