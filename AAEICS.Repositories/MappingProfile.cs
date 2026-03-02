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
        CreateMap<Personnel, PersonnelDTO>();

        CreateMap<PersonnelDTO, Personnel>()
            .ForMember(dest => dest.Rank, opt => opt.Ignore())
            .ForMember(dest => dest.Position, opt => opt.Ignore());

        // 3. Сертифікати надходження
        CreateMap<IncomingCertificate, IncomingCertificateDTO>();

        CreateMap<IncomingCertificateDTO, IncomingCertificate>()
            .ForMember(dest => dest.IncCertificateId, opt => opt.Ignore())
            .ForMember(dest => dest.ApprovePersonId, opt => opt.MapFrom(src => src.ApprovePerson.PersonId))
            .ForMember(dest => dest.DeliveryCompanyId, opt => opt.MapFrom(src => src.DeliveryCompany.InstanceId))
            .ForMember(dest => dest.DonorId, opt => opt.MapFrom(src => src.Donor.InstanceId))
            .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.Recipient.InstanceId))
            .ForMember(dest => dest.ReasonId, opt => opt.MapFrom(src => src.Reason.ReasonId))
            .ForMember(dest => dest.ApprovePerson, opt => opt.Ignore())
            .ForMember(dest => dest.DeliveryCompany, opt => opt.Ignore())
            .ForMember(dest => dest.Donor, opt => opt.Ignore())
            .ForMember(dest => dest.Reason, opt => opt.Ignore())
            .ForMember(dest => dest.Recipient, opt => opt.Ignore());

        CreateMap<IncomingCertificateLine, IncomingCertificateLineDTO>();

        CreateMap<IncomingCertificateLineDTO, IncomingCertificateLine>()
            .ForMember(dest => dest.IncLineId, opt => opt.Ignore())
            .ForMember(dest => dest.CertificateId, opt => opt.Ignore())
            .ForMember(dest => dest.MeasureUnitId, opt => opt.MapFrom(src => src.MeasureUnit.UnitId))
            .ForMember(dest => dest.CategorySentId, opt => opt.MapFrom(src => src.CategorySent.Id))
            .ForMember(dest => dest.CategoryReceivedId, opt => opt.MapFrom(src => src.CategoryReceived.Id))
            .ForMember(dest => dest.MeasureUnit, opt => opt.Ignore())
            .ForMember(dest => dest.CategorySent, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryReceived, opt => opt.Ignore())
            .ForMember(dest => dest.Certificate, opt => opt.Ignore());

        // 4. Сертифікати видачі (Відразу виправляємо на майбутнє)
        CreateMap<IssuanceCertificate, IssuanceCertificateDTO>();

        CreateMap<IssuanceCertificateDTO, IssuanceCertificate>()
            .ForMember(dest => dest.IssueCertificateId, opt => opt.Ignore())
            .ForMember(dest => dest.ApprovePersonId, opt => opt.MapFrom(src => src.ApprovePerson.PersonId))
            .ForMember(dest => dest.DeliveryCompanyId, opt => opt.MapFrom(src => src.DeliveryCompany.InstanceId))
            .ForMember(dest => dest.DonorId, opt => opt.MapFrom(src => src.Donor.InstanceId))
            .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.Recipient.InstanceId))
            .ForMember(dest => dest.ReasonId, opt => opt.MapFrom(src => src.Reason.ReasonId))
            .ForMember(dest => dest.ApprovePerson, opt => opt.Ignore())
            .ForMember(dest => dest.DeliveryCompany, opt => opt.Ignore())
            .ForMember(dest => dest.Donor, opt => opt.Ignore())
            .ForMember(dest => dest.Reason, opt => opt.Ignore())
            .ForMember(dest => dest.Recipient, opt => opt.Ignore());

        CreateMap<IssueCertificateLine, IssueCertificateLineDTO>();

        CreateMap<IssueCertificateLineDTO, IssueCertificateLine>()
            .ForMember(dest => dest.IssueLineId, opt => opt.Ignore())
            .ForMember(dest => dest.CertificateId, opt => opt.Ignore())
            .ForMember(dest => dest.MeasureUnitId, opt => opt.MapFrom(src => src.MeasureUnit.UnitId))
            .ForMember(dest => dest.CategorySentId, opt => opt.MapFrom(src => src.CategorySent.Id))
            .ForMember(dest => dest.CategoryReceivedId, opt => opt.MapFrom(src => src.CategoryReceived.Id))
            .ForMember(dest => dest.MeasureUnit, opt => opt.Ignore())
            .ForMember(dest => dest.CategorySent, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryReceived, opt => opt.Ignore())
            .ForMember(dest => dest.Certificate, opt => opt.Ignore());
    }
}