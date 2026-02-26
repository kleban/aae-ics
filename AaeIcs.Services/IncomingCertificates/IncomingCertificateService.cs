using AAEICS.Core.DTO.Certificates;
using AAEICS.Core.Contracts;
using AAEICS.Core.Contracts.Repositories;
using AAEICS.Core.Contracts.Services;

namespace AAEICS.Services.IncomingCertificates;

public class IncomingCertificateService(IUnitOfWork unitOfWork) : IIncomingCertificateService
{
    public async Task<IEnumerable<IncomingCertificateDTO>> GetIncomingCertificates()
    {
        return await unitOfWork.IncomingCertificatesRepository.GetAllAsync();
    }
    
    public IncomingCertificateDTO GetIncomingCertificate(int id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> AddIncomingCertificateAsync(IncomingCertificateDTO incomingCertificate, IEnumerable<IncomingCertificateLineDTO> incomingCertificateLines)
    {
        await unitOfWork.IncomingCertificatesRepository.AddAsync(incomingCertificate, incomingCertificateLines);
        await unitOfWork.CompleteAsync();
        return true;
    }
}