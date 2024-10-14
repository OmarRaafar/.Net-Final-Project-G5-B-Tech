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
    public class ProductSpecificationService
    {
        private readonly IProductSpecificationRepository _specificationRepository;
        private readonly IMapper _mapper;

        public ProductSpecificationService(IProductSpecificationRepository specificationRepository, IMapper mapper)
        {
            _specificationRepository = specificationRepository;
            _mapper = mapper;
        }

        public async Task<ResultView<ProductSpecificationDto>> AddSpecificationAsync(ProductSpecificationDto specificationDto, int userId)
        {
            var specification = _mapper.Map<ProductSpecificationsB>(specificationDto);
            //specification.CreatedBy = userId;

            await _specificationRepository.AddAsync(specification);
            return ResultView<ProductSpecificationDto>.Success(specificationDto);
           
        }

        public async Task<ResultView<ProductSpecificationDto>> UpdateSpecificationAsync(ProductSpecificationDto specificationDto, int userId)
        {
            var existingSpec = await _specificationRepository.GetByIdAsync(specificationDto.Id);
            if (existingSpec == null)
            {
                return ResultView<ProductSpecificationDto>.Failure("Specification not found.");
              
            }

            //existingSpec.UpdatedBy = userId;
            _mapper.Map(specificationDto, existingSpec);

            await _specificationRepository.UpdateAsync(existingSpec);
            return ResultView<ProductSpecificationDto>.Success(specificationDto);
            
        }

        public async Task<ResultView<ProductSpecificationDto>> DeleteSpecificationAsync(int id, int userId)
        {
            await _specificationRepository.DeleteAsync(id);
            return  ResultView<ProductSpecificationDto>.Success(null);
        }

        public IQueryable<ProductSpecificationsB> GetSpecificationsByProductId(int productId)
        {
            return _specificationRepository.GetSpecificationsByProductId(productId);
        }
    }
}
