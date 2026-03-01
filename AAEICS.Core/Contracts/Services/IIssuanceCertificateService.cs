using AAEICS.Core.DTO.Certificates;
 
namespace AAEICS.Core.Contracts.Services;

public interface IIssuanceCertificateService
{
    Task<IEnumerable<IssuanceCertificateDTO>> GetIssuanceCertificates();
    IssuanceCertificateDTO GetIssuanceCertificate(int id);
    Task<bool> AddIssuanceCertificateAsync(IssuanceCertificateDTO incomingCertificate);
}