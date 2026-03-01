using AAEICS.Core.DTO.General;

namespace AAEICS.Core.Contracts.Repositories;

public interface IUnitOfWork : IDisposable
{
    IIncomingCertificateRepository IncomingCertificates { get; }
    IGenericRepository<RankDTO> Ranks { get; }
    IGenericRepository<PositionDTO> Positions { get; }
    IGenericRepository<ReasonDTO> Reasons { get; }
    
    Task CompleteAsync();
}
