using AAEICS.Core.Contracts.Repositories;

using AAEICS.Database.Context;

namespace AAEICS.Repositories;

public class UnitOfWork(AAEICSDbContext dbContext, IIncomingCertificateRepository incomingCertificateRepository) : IUnitOfWork
{
    public IIncomingCertificateRepository IncomingCertificatesRepository { get; set; } = incomingCertificateRepository;

    public IGenericRepository<T> Repository<T>() where T : class
    {
        return new GenericRepository<T>(dbContext);
    }

    // Ось тут відбувається справжнє збереження
    public async Task CompleteAsync()
    {
        try
        {
            // Спробуємо зберегти дані
            await dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            // Якщо сталася помилка (наприклад ForeignKey) - примусово очищаємо 
            // пам'ять DbContext від усіх "завислих" змін.
            dbContext.ChangeTracker.Clear();

            // Перекидаємо помилку далі, щоб твоя ViewModel могла її спіймати 
            // і показати користувачу через MessageBox.
            throw;
        }
    }

    public void Dispose()
    {
        dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}