using AAEICS.Shared.Dto;

namespace AAEICS.Services.IncomingCertificates;

public interface IIncomingCertificateService
{
    public Task<List<IncomingCertificateDto>> GetIncomingCertificates();
    
    public IncomingCertificateDto GetIncomingCertificate(int id);
    
    public IncomingCertificateDto AddIncomingCertificate(IncomingCertificateDto incomingCertificate);
}