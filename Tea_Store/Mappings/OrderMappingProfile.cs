using AutoMapper;
using Tea_Store.Models;
using ViewModels.OrderController;

namespace Tea_Store.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            // OrderCreateViewModel - Order
            CreateMap<OrderCreateViewModel, Order>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"));

            // Order - OrderViewViewModel
            CreateMap<Order, OrderViewViewModel>()
            .ForMember(dest => dest.Teas, opt => opt.MapFrom(src => src.OrderTeas.Select(ot => new TeaViewModel
            {
                Id = ot.Tea.Id,
                Title = ot.Tea.Title,
                Price = ot.Tea.Price
            }).ToList()));
        }
    }
}
