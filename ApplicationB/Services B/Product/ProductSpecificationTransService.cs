using ApplicationB.Contracts_B.Product;
using ApplicationB.Services_B.General;
using AutoMapper;
using DTOsB.Product;
using DTOsB.Shared;
using ModelsB.Product_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.Product
{
    public class ProductSpecificationTransService: IProductSpecificationTransService
    {
        private readonly IProductTranslationRepository _translationRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILanguageService _languageService;

        public ProductSpecificationTransService(IProductTranslationRepository translationRepository, IMapper mapper,
            IUserService userService, ILanguageService languageService)
        {
            _translationRepository = translationRepository;
            _mapper = mapper;
            _userService = userService;
            _languageService = languageService;
        }

        public async Task<ResultView<ProductSpecificationTranslationDto>> AddTranslationAsync
            (ProductSpecificationTranslationDto translationDto)
        {
            var translation = _mapper.Map<ProductSpecificationTranslationB>(translationDto);

            translation.ProductSpecification.Product.CreatedBy = _userService.GetCurrentUserId();
            translation.Language.Code = _languageService.GetCurrentLanguageCode();


            //await _translationRepository.AddAsync(translation);
            return ResultView<ProductSpecificationTranslationDto>.Success(translationDto);
        }

        public async Task<ResultView<IQueryable<ProductSpecificationTranslationDto>>> GetSpecificationsTransByProductIdAsync(int productId)
        {
            var translations = await _translationRepository.GetTranslationsByProductId(productId);
            if (!translations.Any())
            {
                return ResultView<IQueryable<ProductSpecificationTranslationDto>>.Failure("No Spec translations found for this product.");
            }

            var translationDtos = _mapper.Map<IQueryable<ProductSpecificationTranslationDto>>(translations);
            return ResultView<IQueryable<ProductSpecificationTranslationDto>>.Success(translationDtos);
        }



        public async Task<ResultView<IQueryable<ProductSpecificationTranslationDto>>>
        GetTranslationsBySpecificationIdAsync(int specificationId)
        {
            var translations = await _translationRepository.GetByIdAsync(specificationId);


            var translationDtos = _mapper.Map<IQueryable<ProductSpecificationTranslationDto>>(translations);

            return ResultView<IQueryable<ProductSpecificationTranslationDto>>.Success(translationDtos);
        }



        public async Task<ResultView<ProductSpecificationTranslationDto>>
            UpdateTranslationAsync(ProductSpecificationTranslationDto translationDto)
        {
            var existingTranslation = await _translationRepository.GetByIdAsync(translationDto.Id);
            if (existingTranslation == null)
            {
                return ResultView<ProductSpecificationTranslationDto>.Failure("Translation not found.");
            }

         
            _mapper.Map(translationDto, existingTranslation);

            await _translationRepository.UpdateAsync(existingTranslation);

            return ResultView<ProductSpecificationTranslationDto>.Success(translationDto);
        }

        public async Task<ResultView<ProductSpecificationTranslationDto>>
            GetSpecificationByIdAsync(int id)
        {
            var translation = await _translationRepository.GetByIdAsync(id);
            if (translation == null)
            {
                return ResultView<ProductSpecificationTranslationDto>.Failure("Specification translation not found.");
            }

            var translationDto = _mapper.Map<ProductSpecificationTranslationDto>(translation);

            return ResultView<ProductSpecificationTranslationDto>.Success(translationDto);
        }
    }
}
