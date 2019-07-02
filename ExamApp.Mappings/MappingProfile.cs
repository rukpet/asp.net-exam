using AutoMapper;
using ExamApp.Dto;
using ExamApp.Models;

namespace ExamApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleDto>();
            CreateMap<ArticleDto, Article>();

            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentDto, Payment>();

            CreateMap<BillingAddresse, BillingAddresseDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => new CountryDto { Geo = src.Country }));
            CreateMap<BillingAddresseDto, BillingAddresse>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Geo));

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
        }
    }
}
