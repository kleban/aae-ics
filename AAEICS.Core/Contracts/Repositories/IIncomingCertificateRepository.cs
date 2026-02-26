using System.Linq.Expressions;
using AAEICS.Core.DTO.Certificates;

namespace AAEICS.Core.Contracts.Repositories;

public interface IIncomingCertificateRepository
{
    Task<IEnumerable<IncomingCertificateDTO>> GetAllAsync();
    
    Task AddAsync(IncomingCertificateDTO dto, IEnumerable<IncomingCertificateLineDTO> linesDto);
    // Оскільки ми успадкували IGenericRepository, методи GetAllAsync, GetByIdAsync і т.д. вже тут є!
    
    // Якщо тобі потрібні специфічні запити, ти можеш додати їх сюди. Наприклад:
    // Task<IncomingCertificate?> GetByCertificateNumberAsync(string number);
}