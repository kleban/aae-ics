using System.Linq.Expressions;
using AAEICS.Core.Contracts.Repositories;
using AAEICS.Core.DTO.Certificates;
using AAEICS.Core.DTO.General;
using AAEICS.Database.Context;
using AAEICS.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AAEICS.Repositories.IncomingCertificates;

public class IncomingCertificateRepository(AAEICSDbContext dbContext)
    : GenericRepository<IncomingCertificate>(dbContext), IIncomingCertificateRepository
{
    private readonly AAEICSDbContext _dbContext = dbContext;
    
    public async Task<IEnumerable<IncomingCertificateDTO>> GetAllAsync()
    {
        // 1. Беремо дані з БД разом із зв'язками (жадібне завантаження)
        var entities = await _dbContext.IncomingCertificates
            .Include(c => c.IncomingCertificateLines)
            .Include(incomingCertificate => incomingCertificate.ApprovePersonNavigation)
            .ThenInclude(personnel => personnel.PositionNavigation)
            .Include(incomingCertificate => incomingCertificate.ApprovePersonNavigation)
            .ThenInclude(personnel => personnel.RankNavigation) // Підтягуємо рядки сертифіката
            .ToListAsync();

        // 2. Мапимо моделі БД у DTO (щоб віддати їх у Core)
        // Примітка: для великих проектів тут зручно використовувати AutoMapper
        var incomingCertificateDTOs = entities.Select(entity => new IncomingCertificateDTO
        {
            IncCertificateId = entity.IncCertificateId,
            Edrpou = entity.Edrpou,
            ApprovePerson = new PersonnelDTO()
            {
                PersonId = entity.ApprovePerson,
                FirstName = entity.ApprovePersonNavigation.FirstName,
                LastName = entity.ApprovePersonNavigation.LastName,
                MiddleName = entity.ApprovePersonNavigation.MiddleName,
                Position = new PositionDTO()
                {
                    PositionId = entity.ApprovePersonNavigation.PositionNavigation.PositionId,
                    Name = entity.ApprovePersonNavigation.PositionNavigation.Name
                },
                Rank = new RankDTO()
                {
                    RankId = entity.ApprovePersonNavigation.RankNavigation.RankId,
                    Name = entity.ApprovePersonNavigation.RankNavigation.Name
                }
            },
            ApproveDate = entity.ApproveDate,
            RegistrationDate = entity.RegistrationDate,
            RegistrationPlace = entity.RegistrationPlace,
            TransferDateStart = entity.TransferDateStart,
            TransferDateEnd = entity.TransferDateEnd,
            DonorId = entity.DonorId,
            RecipientId = entity.RecipientId,
            DeliveryCompany = entity.DeliveryCompany,
            Reason = entity.Reason
        });

        return incomingCertificateDTOs;
    }
    
    public async Task AddAsync(IncomingCertificateDTO incomingCertificate, IEnumerable<IncomingCertificateLineDTO> incomingCertificateLines)
    {
        // 1. Створюємо сутність для БД (БЕЗ IncCertificateId, база згенерує його сама)
        IncomingCertificate incomingCertificateEntity = new()
        {
            Edrpou = incomingCertificate.Edrpou,
            // Отримуємо ID з об'єкта або 0, якщо він відсутній
            ApprovePerson = incomingCertificate.ApprovePerson?.PersonId ?? 0, 
            ApproveDate = incomingCertificate.ApproveDate,
            RegistrationDate = incomingCertificate.RegistrationDate,
            RegistrationPlace = incomingCertificate.RegistrationPlace,
            TransferDateStart = incomingCertificate.TransferDateStart,
            TransferDateEnd = incomingCertificate.TransferDateEnd,
            DonorId = incomingCertificate.DonorId,
            RecipientId = incomingCertificate.RecipientId,
            DeliveryCompany = incomingCertificate.DeliveryCompany,
            Reason = incomingCertificate.Reason,
            
            // ВАЖЛИВО: Ініціалізуємо список перед додаванням
            IncomingCertificateLines = new List<IncomingCertificateLine>() 
        };

        // 2. Додаємо рядки сертифіката
        var incomingCertificateLineDtos = incomingCertificateLines.ToList();
        foreach (var lineDto in incomingCertificateLineDtos)
        {
            IncomingCertificateLine lineEntity = new()
            {
                // БЕЗ IncLineId та CertificateId - EF Core сам зв'яже їх при збереженні
                OrdinalNumber = lineDto.OrdinalNumber,
                Name = lineDto.Name,
                NomenclatureCode = lineDto.NomenclatureCode,
                BatchNumber = lineDto.BatchNumber,
                MeasureUnit = lineDto.MeasureUnit?.UnitId ?? 0, // Безпечне отримання ID
                PricePerUnit = lineDto.PricePerUnit,
                QuantitySent = lineDto.QuantitySent,
                CategorySent = lineDto.CategorySent,
                QuantityReceived = lineDto.QuantityReceived,
                CategoryReceived = lineDto.CategoryReceived,
                Notes = lineDto.Notes,
                MadeIn = lineDto.MadeIn
            };
            incomingCertificateEntity.IncomingCertificateLines.Add(lineEntity);
        }
        
        await DbContext.IncomingCertificates.AddAsync(incomingCertificateEntity);
    }
}