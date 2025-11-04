using AutoMapper;
using OrderApp.Domain.DTOs;
using OrderApp.Domain.Entities;

namespace OrderApp.Business.Mapping;

public class OrdenMappingProfile : Profile
{
    public OrdenMappingProfile()
    {
        CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<OrderItem, OrderItemResponseDto>()
            .ForMember(des => des.SubTotal,
                opt => opt.MapFrom(src =>
                    src.Quantity * src.Price));
    }
}