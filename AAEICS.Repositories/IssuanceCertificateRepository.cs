using AAEICS.Core.Contracts.Repositories;
using AAEICS.Core.DTO.Certificates;
using AAEICS.Database.Context;
using AAEICS.Database.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAEICS.Repositories;

public class IssuanceCertificateRepository(AAEICSDbContext dbContext, IMapper mapper)
    : GenericRepository<IssuanceCertificate, IssuanceCertificateDTO>(dbContext, mapper), IIssuanceCertificateRepository
{
    protected override IQueryable<IssuanceCertificate> GetBaseQuery()
    {
        return DbSet
            .Include(c => c.ApprovePerson)
                .ThenInclude(p => p.Position)
            .Include(c => c.ApprovePerson)
                .ThenInclude(p => p.Rank)
            .Include(c => c.DeliveryCompany)
            .Include(c => c.Donor)
            .Include(c => c.Recipient)
            .Include(c => c.Reason)
            .Include(c => c.IssueCertificateLines)
                .ThenInclude(l => l.MeasureUnit);
    }
    
    // Перевизначаємо метод додавання для контролю над збереженням графа об'єктів
    public override async Task AddAsync(IssuanceCertificateDTO dto)
    {
        // 1. Мапимо DTO в сутність
        var entity = Mapper.Map<IssuanceCertificate>(dto);

        // 2. Відділяємо рядки від головного об'єкта
        var lines = entity.IssueCertificateLines.ToList();
        entity.IssueCertificateLines.Clear();

        // 3. Додаємо головний об'єкт (Сертифікат) в контекст бази даних.
        // EF починає його відслідковувати (Track).
        await DbSet.AddAsync(entity);

        // 4. Явно додаємо рядки, вказуючи їх зв'язок з головним об'єктом.
        foreach (var line in lines)
        {
            // Явно пов'язуємо рядок з сертифікатом через об'єкт-посилання.
            // Завдяки цьому EF знає, що ці рядки належать щойно доданому entity,
            // і під час UnitOfWork.CompleteAsync() сам проставить правильний CertificateId.
            line.Certificate = entity; 
            
            // Додаємо рядок до контексту
            await DbContext.Set<IssueCertificateLine>().AddAsync(line);
        }
    }
}