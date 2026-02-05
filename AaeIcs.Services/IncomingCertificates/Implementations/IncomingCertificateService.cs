using AAEICS.Shared.DTOs;
using AAEICS.Services.IncomingCertificates.Contracts;

namespace AAEICS.Services.IncomingCertificates.Implementations;

public class IncomingCertificateService: IIncomingCertificateService
{
    public async Task<List<IncomingCertificateDTO>> GetIncomingCertificates()
    {
        List<IncomingCertificateDTO> incomingCertificates =
        [
            new IncomingCertificateDTO
            {
                IncCertificateId = 1,
                Edrpou = 12345678,
                ApprovePerson = "John Smith",
                ApproveDate = DateTime.Now.AddDays(-5),
                RegistrationDate = DateTime.Now.AddDays(-10),
                RegistrationPlace = "Kyiv, Ukraine",
                TransferDateStart = DateTime.Now.AddDays(-3),
                TransferDateEnd = DateTime.Now.AddDays(7),
                Donor = "Acme Corporation",
                Recipient = "Tech Solutions Ltd",
                Deliver = "Express Delivery Service",
                Reason = "Equipment transfer for project implementation"
            }
        ];
        return await Task.FromResult(incomingCertificates);
    }
    
    public IncomingCertificateDTO GetIncomingCertificate(int id)
    {
        throw new NotImplementedException();
    }
    
    public IncomingCertificateDTO AddIncomingCertificate(IncomingCertificateDTO incomingCertificate)
    {
        throw new NotImplementedException();
    }
}