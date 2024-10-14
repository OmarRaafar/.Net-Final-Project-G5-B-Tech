using ApplicationB.Contracts_B;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DTOsB.Product;
using DTOsB.Shared;
using Microsoft.Extensions.Logging;
using ModelsB.Product_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.Product
{
public class ProductService//:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly int _currentUserId;

        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger, int currentUserId)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _currentUserId = currentUserId;
        }

        public async Task<ResultView<ProductDto>> CreateProductAsync(ProductDto productDto)
        {
            if (productDto.Id > 0)
                return ResultView<ProductDto>.Failure("Product already exists. Use update to modify it.");

            if (productDto.Price < 0)
                return ResultView<ProductDto>.Failure("Product price must be a positive value.");

            var product = _mapper.Map<ProductB>(productDto);
            product.CreatedBy = _currentUserId;
            product.Created = DateTime.Now;

            await _productRepository.AddAsync(product);
            return ResultView<ProductDto>.Success(productDto);
        }

        public async Task<ResultView<ProductDto>> UpdateProductAsync(ProductDto productDto)
        {
            if (productDto == null)
                return ResultView<ProductDto>.Failure("Product data cannot be null.");

            var existingProduct = await _productRepository.GetByIdAsync(productDto.Id);
            if (existingProduct == null)
                return ResultView<ProductDto>.Failure("Product not found. Unable to update.");

            if (productDto.Price < 0)
                return ResultView<ProductDto>.Failure("Product price must be a positive value.");

            _mapper.Map(productDto, existingProduct);
            existingProduct.UpdatedBy = _currentUserId;
            existingProduct.Updated = DateTime.Now;

            await _productRepository.UpdateAsync(existingProduct);
            return ResultView<ProductDto>.Success(productDto);
        }

        public async Task<ResultView<ProductDto>> DeleteProductAsync(int id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return ResultView<ProductDto>.Failure("Product not found. Unable to delete.");

            existingProduct.IsDeleted = true;
            existingProduct.UpdatedBy = _currentUserId;
            existingProduct.Updated = DateTime.Now;

            await _productRepository.UpdateAsync(existingProduct);
            return ResultView<ProductDto>.Success(null);
        }

        public async Task<ResultView<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return ResultView<ProductDto>.Failure("Product not found.");

            var productDto = _mapper.Map<ProductDto>(product);
            return ResultView<ProductDto>.Success(productDto);
        }

        public IQueryable<ProductDto> GetAllProducts()
        {
            var products = _productRepository.GetAll();
            return products.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
        }

        public IQueryable<ProductDto> SearchProductsByName(string name)
        {
            var products = _productRepository.SearchByName(name);
            return products.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
        }

        // Handling Related Entities

        //public async Task<ResultView<ProductDto>> AddImagesToProductAsync(int productId, IEnumerable<ProductImageDto> imageDtos)
        //{
        //    var images = _mapper.Map<IEnumerable<ProductImageB>>(imageDtos);
        //    await _productRepository.AddImagesAsync(productId, images);
        //    return await GetProductByIdAsync(productId); // Return updated product
        //}

        //public async Task<ResultView<ProductDto>> RemoveImageFromProductAsync(int productId, int imageId)
        //{
        //    await _productRepository.RemoveImageAsync(productId, imageId);
        //    return await GetProductByIdAsync(productId); // Return updated product
        //}

        //public async Task<ResultView<ProductDto>> AddTranslationsToProductAsync(int productId, IEnumerable<ProductTranslationDto> translationDtos)
        //{
        //    var translations = _mapper.Map<IEnumerable<ProductTranslationB>>(translationDtos);
        //    await _productRepository.AddTranslationsAsync(productId, translations);
        //    return await GetProductByIdAsync(productId); // Return updated product
        //}

        //public async Task<ResultView<ProductDto>> RemoveTranslationFromProductAsync(int productId, int translationId)
        //{
        //    await _productRepository.RemoveTranslationAsync(productId, translationId);
        //    return await GetProductByIdAsync(productId); // Return updated product
        //}

        //public async Task<ResultView<ProductDto>> AddSpecificationsToProductAsync(int productId, IEnumerable<ProductSpecificationsDto> specificationDtos)
        //{
        //    var specifications = _mapper.Map<IEnumerable<ProductSpecificationsB>>(specificationDtos);
        //    await _productRepository.AddSpecificationsAsync(productId, specifications);
        //    return await GetProductByIdAsync(productId); // Return updated product
        //}

        //public async Task<ResultView<ProductDto>> RemoveSpecificationFromProductAsync(int productId, int specificationId)
        //{
        //    await _productRepository.RemoveSpecificationAsync(productId, specificationId);
        //    return await GetProductByIdAsync(productId); // Return updated product
        //}

        //public IQueryable<ProductSpecificationsB> GetSpecificationsByProductId(int productId)
        //{
        //    return _productRepository.GetSpecificationsByProductId(productId);
        //}
    }

}

