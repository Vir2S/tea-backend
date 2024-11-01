using AutoMapper;
using Tea_Store.Models;
using ViewModels.CartController;

namespace Tea_Store.Mappings
{
    public class ShoppingCartMappingProfile : Profile
    {
        public ShoppingCartMappingProfile()
        {
            CreateMap<CartItemViewModel, CartItem>()
                .ForMember(dest => dest.TeaId, opt => opt.MapFrom(src => src.TeaId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<CartItemUpdateViewModel, CartItem>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<ShoppingCart, CartViewViewModel>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CartItem, CartItemViewModel>()
                .ForMember(dest => dest.TeaId, opt => opt.MapFrom(src => src.TeaId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }


    }
}
