using AAEICS.Core.Contracts.Repositories;
using AAEICS.Core.DTO.Certificates;
using AAEICS.Database.Context;
using AAEICS.Database.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAEICS.Repositories;

public class IncomingCertificateRepository(AAEICSDbContext dbContext, IMapper mapper)
    : GenericRepository<IncomingCertificate, IncomingCertificateDTO>(dbContext, mapper), IIncomingCertificateRepository
{
    protected override IQueryable<IncomingCertificate> GetBaseQuery()
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
            .Include(c => c.IncomingCertificateLines)
                .ThenInclude(l => l.MeasureUnit);
    }
}