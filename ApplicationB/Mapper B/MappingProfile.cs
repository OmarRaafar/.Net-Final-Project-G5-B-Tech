using AutoMapper;
using DTOsB.OrderBDTOs.OrderItemDTO;
using DTOsB.OrderBDTOs.PaymentDTO;
using DTOsB.OrderBDTOs.ShippingDTO;
using DTOsB.OrderDTO;
using DTOsB.Product;
using ModelsB.Order_B;
using ModelsB.Product_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationB.Mapper_B
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductB, ProductDto>().ReverseMap(); 
            CreateMap<ProductImageB, ProductImageDto>().ReverseMap();
            CreateMap<ProductTranslationB, ProductTranslationDto>().ReverseMap();
            CreateMap<ProductSpecificationsB, ProductSpecificationDto>().ReverseMap();
            CreateMap<ProductSpecificationTranslationB, ProductSpecificationTranslationDto>().ReverseMap();

            CreateMap<OrderB, AddOrUpdateOrderBDTO>().ReverseMap();
            CreateMap<OrderB, SelectOrderBDTO>().ReverseMap();

            CreateMap<OrderItemB, AddOrUpdateOrderItemBDTO>().ReverseMap();
            CreateMap<OrderItemB, SelectOrderItemBDTO>().ReverseMap();

            CreateMap<PaymentB, AddOrUpdatePaymentBDTO>().ReverseMap();
            CreateMap<PaymentB, SelectPaymentBDTO>().ReverseMap();

            CreateMap<ShippingB, AddOrUpdateShippingBDTO>().ReverseMap();
            CreateMap<ShippingB, SelectShippingBDTO>().ReverseMap();
        }
    
    }
}
