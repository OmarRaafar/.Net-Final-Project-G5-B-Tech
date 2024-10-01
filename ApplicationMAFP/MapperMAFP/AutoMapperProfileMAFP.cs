using AutoMapper;
using DTOsMAFP.Product;
using ModelsMAFP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationMAFP.MapperMAFP
{
    public class AutoMapperProfileMAFP : Profile
    {
        public AutoMapperProfileMAFP()
        {

            //product Mapping
            CreateMap<ProductMAFP, GetProductDTO>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Categories.FirstOrDefault().CategoryName)).ReverseMap();

            CreateMap<ProductMAFP,CreateOrUpdateProductDTO>().ReverseMap();
        }
    }
}
