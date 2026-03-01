using AAEICS.Core.Contracts.Repositories;
using AAEICS.Core.DTO.General;
using AAEICS.Database.Context;
using AAEICS.Database.Models;
using AutoMapper;

namespace AAEICS.Repositories;

public class UnitOfWork(AAEICSDbContext dbContext, IMapper mapper, IIncomingCertificateRepository incomingCertificatesRepository): IUnitOfWork
{
    public IIncomingCertificateRepository IncomingCertificates { get; } = incomingCertificatesRepository;
    
    public IGenericRepository<RankDTO> Ranks => 
        field ??= new DictionaryDataRepository<Rank, RankDTO>(dbContext, mapper);

    public IGenericRepository<PositionDTO> Positions => 
        field ??= new DictionaryDataRepository<Position, PositionDTO>(dbContext, mapper);

    public IGenericRepository<ReasonDTO> Reasons => 
        field ??= new DictionaryDataRepository<Reason, ReasonDTO>(dbContext, mapper);

    public async Task CompleteAsync()
    {
        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            dbContext.ChangeTracker.Clear();
            throw;
        }
    }

    public void Dispose()
    {
        dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
