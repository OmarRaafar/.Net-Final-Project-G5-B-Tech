using AutoMapper;
using DTOsB.Account;
using DTOsB.Category;
using DTOsB.Product;
using ModelsB.Authentication_and_Authorization_B;
using ModelsB.Category_B;
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
            CreateMap<RegisterDto, ApplicationUserB>().ReverseMap();
            CreateMap<LoginDTO, ApplicationUserB>().ReverseMap();


            CreateMap<ProductB, ProductDto>().ReverseMap(); 
            CreateMap<ProductImageB, ProductImageDto>().ReverseMap();
            CreateMap<ProductImageB, ProductImageCreateOrUpdateDto>().ReverseMap();
            CreateMap<ProductTranslationB, ProductTranslationDto>().ReverseMap();
            CreateMap<ProductSpecificationsB, ProductSpecificationDto>().ReverseMap();
            CreateMap<ProductSpecificationTranslationB, ProductSpecificationTranslationDto>().ReverseMap();
            CreateMap<SpecificationStore, SpecificationStoreDto>().ReverseMap();
            CreateMap<ReviewB, ReviewDto>().ReverseMap();


            CreateMap<CategoryB, GetAllCategoriesDTO>().ReverseMap();
            CreateMap<CategoryB, CreateOrUpdateCategoriesDTO>().ReverseMap();
            CreateMap<CategoryTranslationB, CategoryTranslationDTO>().ReverseMap();
            CreateMap<CategoryTranslationB, CreateCategoryTranslationDto>().ReverseMap();

        }
    
    }
}
