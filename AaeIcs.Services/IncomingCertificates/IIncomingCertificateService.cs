using AAEICS.Shared.DTOs;

namespace AAEICS.Services.IncomingCertificates;

public interface IIncomingCertificateService
{
    public Task<List<IncomingCertificateDTO>> GetIncomingCertificates();
    
    public IncomingCertificateDTO GetIncomingCertificate(int id);
    
    public IncomingCertificateDTO AddIncomingCertificate(IncomingCertificateDTO incomingCertificate);
}