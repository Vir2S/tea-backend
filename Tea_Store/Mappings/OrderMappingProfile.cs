﻿using AutoMapper;
using Tea_Store.DTOs.OrdersDTO;
using Tea_Store.Models;

namespace Tea_Store.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            // OrderCreateDTO - Order
            CreateMap<OrderCreateDTO, Order>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"));

            // Order - OrderViewDTO
            CreateMap<Order, OrderViewDTO>()
            .ForMember(dest => dest.Teas, opt => opt.MapFrom(src => src.OrderTeas.Select(ot => new TeaDTO
            {
                Id = ot.Tea.Id,
                Title = ot.Tea.Title,
                Price = ot.Tea.Price
            }).ToList()));
        }
    }
}
