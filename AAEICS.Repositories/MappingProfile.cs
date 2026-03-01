using AAEICS.Core.DTO.Certificates;
using AAEICS.Core.DTO.General;
using AAEICS.Database.Models;
using AutoMapper;

namespace AAEICS.Repositories;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // 1. Базові довідники (маппінг в обидві сторони)
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Position, PositionDTO>().ReverseMap();
        CreateMap<Rank, RankDTO>().ReverseMap();
        CreateMap<Reason, ReasonDTO>().ReverseMap();
        CreateMap<MeasureUnit, MeasureUnitDTO>().ReverseMap();
        CreateMap<TransferInstance, TransferInstanceDTO>().ReverseMap();

        // 2. Персонал
        CreateMap<Personnel, PersonnelDTO>(); // Оскільки імена властивостей (Rank, Position) збігаються, AutoMapper зробить все сам

        CreateMap<PersonnelDTO, Personnel>()
            // При зворотньому маппінгу ми ігноруємо навігаційні об'єкти, щоб Entity Framework не намагався створити їх заново
            .ForMember(dest => dest.Rank, opt => opt.Ignore())
            .ForMember(dest => dest.Position, opt => opt.Ignore());

        // 3. Сертифікати надходження
        CreateMap<IncomingCertificate, IncomingCertificateDTO>();

        CreateMap<IncomingCertificateDTO, IncomingCertificate>()
            .ForMember(dest => dest.ApprovePerson, opt => opt.Ignore())
            .ForMember(dest => dest.DeliveryCompany, opt => opt.Ignore())
            .ForMember(dest => dest.Donor, opt => opt.Ignore())
            .ForMember(dest => dest.Reason, opt => opt.Ignore())
            .ForMember(dest => dest.Recipient, opt => opt.Ignore());

        CreateMap<IncomingCertificateLine, IncomingCertificateLineDTO>();

        CreateMap<IncomingCertificateLineDTO, IncomingCertificateLine>()
            .ForMember(dest => dest.MeasureUnit, opt => opt.Ignore())
            .ForMember(dest => dest.CategorySent, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryReceived, opt => opt.Ignore())
            .ForMember(dest => dest.Certificate, opt => opt.Ignore());

        // 4. Сертифікати видачі
        CreateMap<IssuanceCertificate, IssuanceCertificateDTO>();

        CreateMap<IssuanceCertificateDTO, IssuanceCertificate>()
            .ForMember(dest => dest.ApprovePerson, opt => opt.Ignore())
            .ForMember(dest => dest.DeliveryCompany, opt => opt.Ignore())
            .ForMember(dest => dest.Donor, opt => opt.Ignore())
            .ForMember(dest => dest.Reason, opt => opt.Ignore())
            .ForMember(dest => dest.Recipient, opt => opt.Ignore());

        CreateMap<IssueCertificateLine, IssueCertificateLineDTO>();

        CreateMap<IssueCertificateLineDTO, IssueCertificateLine>()
            .ForMember(dest => dest.MeasureUnit, opt => opt.Ignore())
            .ForMember(dest => dest.CategorySent, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryReceived, opt => opt.Ignore())
            .ForMember(dest => dest.Certificate, opt => opt.Ignore());
    }
}