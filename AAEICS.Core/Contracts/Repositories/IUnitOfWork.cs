namespace AAEICS.Core.Contracts.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IIncomingCertificateRepository IncomingCertificatesRepository { get; }
    // Generic для всього іншого (наприклад, Locations, Activities, якщо треба, просто CRUD)
    IGenericRepository<T> Repository<T>() where T : class;

    Task CompleteAsync();
}