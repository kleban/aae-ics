using AAEICS.Core.Contracts.Repositories;
using AAEICS.Core.Contracts.Services;
using AAEICS.Core.DTO.Certificates;

namespace AAEICS.Services;

public class IncomingCertificateService(IUnitOfWork unitOfWork) : IIncomingCertificateService
{
    public async Task<IEnumerable<IncomingCertificateDTO>> GetIncomingCertificates()
    {
        return await unitOfWork.IncomingCertificates.GetAllAsync();
    }
    
    public IncomingCertificateDTO GetIncomingCertificate(int id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> AddIncomingCertificateAsync(IncomingCertificateDTO incomingCertificate)
    {
        await unitOfWork.IncomingCertificates.AddAsync(incomingCertificate);
        await unitOfWork.CompleteAsync();
        return true;
    }
}
