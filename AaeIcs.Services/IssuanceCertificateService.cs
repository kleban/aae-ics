using AAEICS.Core.Contracts.Repositories;
using AAEICS.Core.Contracts.Services;
using AAEICS.Core.DTO.Certificates;

namespace AAEICS.Services;

public class IssuanceCertificateService(IUnitOfWork unitOfWork) : IIssuanceCertificateService
{
    public async Task<IEnumerable<IssuanceCertificateDTO>> GetIssuanceCertificates()
    {
        return await unitOfWork.IssuanceCertificates.GetAllAsync();
    }
    
    public IssuanceCertificateDTO GetIssuanceCertificate(int id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> AddIssuanceCertificateAsync(IssuanceCertificateDTO issuanceCertificate)
    {
        await unitOfWork.IssuanceCertificates.AddAsync(issuanceCertificate);
        await unitOfWork.CompleteAsync();
        return true;
    }
}
