using ApplicationB.Contracts_B.Product;
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
    public class ProductTranslationService
    {
        private readonly IProductTranslationRepository _translationRepository;
        private readonly IMapper _mapper;

        public ProductTranslationService(IProductTranslationRepository translationRepository, IMapper mapper)
        {
            _translationRepository = translationRepository;
            _mapper = mapper;
        }

        public async Task<ResultView<ProductTranslationDto>> AddTranslationAsync(ProductTranslationDto translationDto, int userId)
        {
            var translation = _mapper.Map<ProductTranslationB>(translationDto);
            //translation.CreatedBy = userId;

            await _translationRepository.AddAsync(translation);
            return ResultView<ProductTranslationDto>.Success(translationDto);
      
        }

        public async Task<ResultView<ProductTranslationDto>> UpdateTranslationAsync(ProductTranslationDto translationDto, int userId)
        {
            var existingTranslation = await _translationRepository.GetByIdAsync(translationDto.Id);
            if (existingTranslation == null)
            {
                return ResultView<ProductTranslationDto>.Failure("Translation not found.");
              
            }

            //existingTranslation.UpdatedBy = userId;
            _mapper.Map(translationDto, existingTranslation);

            await _translationRepository.UpdateAsync(existingTranslation);
            return ResultView<ProductTranslationDto>.Success(translationDto);
           
        }

        public async Task<ResultView<ProductTranslationDto>> DeleteTranslationAsync(int id, int userId)
        {
            await _translationRepository.DeleteAsync(id);
            return ResultView<ProductTranslationDto>.Success(null);
        }

        public IQueryable<ProductTranslationB> GetTranslationsByProductId(int productId)
        {
            return _translationRepository.GetTranslationsByProductId(productId);
        }
    }
}
