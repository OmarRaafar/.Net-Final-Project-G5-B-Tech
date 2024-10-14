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
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ProductImageService(IProductImageRepository productImageRepository, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        public async Task<ResultView<ProductImageDto>> AddImageAsync(ProductImageDto productImageDto, int userId)
        {
            var productImage = _mapper.Map<ProductImageB>(productImageDto);
            //productImage.CreatedBy = userId;

            await _productImageRepository.AddAsync(productImage);
            return ResultView<ProductImageDto>.Success(productImageDto);
          
        }

        public async Task<ResultView<ProductImageDto>> UpdateImageAsync(ProductImageDto productImageDto, int userId)
        {
            var existingImage = await _productImageRepository.GetByIdAsync(productImageDto.Id);
            if (existingImage == null)
            {
                return ResultView<ProductImageDto>.Failure("Image not found.");
              
            }

            //existingImage.UpdatedBy = userId;

            _mapper.Map(productImageDto, existingImage);

            await _productImageRepository.UpdateAsync(existingImage);
            return ResultView<ProductImageDto>.Success(productImageDto);
       
        }

        public async Task<ResultView<ProductImageDto>> DeleteImageAsync(int id)
        {
            await _productImageRepository.DeleteAsync(id);
           
            return ResultView<ProductImageDto>.Success(null);
        }

        public IQueryable<ProductImageB> GetImagesByProductId(int productId)
        {
            return _productImageRepository.GetImagesByProductId(productId);
        }
    }
}
