using ApplicationB.Contracts_B.Category;
using AutoMapper;
using DTOsB.Category;
using DTOsB.Shared;
using ModelsB.Category_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.Category
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _repository;
        private readonly IMapper _mapper;

        public ProductCategoryService(IProductCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResultView<IEnumerable<ProductCategoryDto>>> GetAllAsync()
        {
            var productCategories = await _repository.GetAllAsync();
            var productCategoryDtos = _mapper.Map<IEnumerable<ProductCategoryDto>>(productCategories);
            return ResultView<IEnumerable<ProductCategoryDto>>.Success(productCategoryDtos);
        }

        public async Task<ResultView<ProductCategoryDto>> GetByIdAsync(int productId, int categoryId)
        {
            var productCategory = await _repository.GetByIdAsync(productId, categoryId);
            if (productCategory == null)
            {
                return ResultView<ProductCategoryDto>.Failure("Product-Category association not found.");
            }

            var productCategoryDto = _mapper.Map<ProductCategoryDto>(productCategory);
            return ResultView<ProductCategoryDto>.Success(productCategoryDto);
        }

        public async Task<ResultView<ProductCategoryDto>> AddAsync(ProductCategoryDto productCategoryDto)
        {
            try
            {
                var productCategory = _mapper.Map<ProductCategoryB>(productCategoryDto);
                await _repository.AddAsync(productCategory);
                return ResultView<ProductCategoryDto>.Success(productCategoryDto);
            }
            catch (Exception ex)
            {
                return ResultView<ProductCategoryDto>.Failure($"Failed to add Product-Category: {ex.Message}");
            }
        }

        public async Task<ResultView<ProductCategoryDto>> UpdateAsync(ProductCategoryDto productCategoryDto)
        {
            try
            {
                var productCategory = _mapper.Map<ProductCategoryB>(productCategoryDto);
                await _repository.UpdateAsync(productCategory);
                return ResultView<ProductCategoryDto>.Success(productCategoryDto);
            }
            catch (Exception ex)
            {
                return ResultView<ProductCategoryDto>.Failure($"Failed to update Product-Category: {ex.Message}");
            }
        }

        public async Task<ResultView<bool>> DeleteAsync(int productId, int categoryId)
        {
            try
            {
                await _repository.DeleteAsync(productId, categoryId);
                return ResultView<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultView<bool>.Failure($"Failed to delete Product-Category: {ex.Message}");
            }
        }
        public async Task<ResultView<List<ProductCategoryDto>>> GetProductsByCategoryNameAsync(string categoryName)
        {
            var productCategories = await _repository.GetByCategoryNameAsync(categoryName);

            if (!productCategories.Any())
            {
                return ResultView<List<ProductCategoryDto>>.Failure($"No products found for category name '{categoryName}'.");
            }

            var productCategoryDtos = productCategories.Select(pc => _mapper.Map<ProductCategoryDto>(pc)).ToList();

            return ResultView<List<ProductCategoryDto>>.Success(productCategoryDtos);
        }
        public async Task<ResultView<List<ProductCategoryDto>>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var productCategories = await _repository.GetByCategoryIdAsync(categoryId);

            if (!productCategories.Any())
            {
                return ResultView<List<ProductCategoryDto>>.Failure($"No products found for category with ID '{categoryId}'.");
            }

            var productCategoryDtos = productCategories.Select(pc => _mapper.Map<ProductCategoryDto>(pc)).ToList();

            return ResultView<List<ProductCategoryDto>>.Success(productCategoryDtos);
        }
        public async Task<ResultView<List<ProductCategoryDto>>> GetMainCategoriesAsync()
        {
            var mainCategories = await _repository.GetMainCategoriesAsync();

            if (!mainCategories.Any())
            {
                return ResultView<List<ProductCategoryDto>>.Failure("No main categories found.");
            }

            var mainCategoryDtos = mainCategories.Select(pc => _mapper.Map<ProductCategoryDto>(pc)).ToList();

            return ResultView<List<ProductCategoryDto>>.Success(mainCategoryDtos);
        }
        public async Task<ResultView<List<ProductCategoryDto>>> GetSubCategoriesAsync()
        {
            var subCategories = await _repository.GetSubCategoriesAsync();

            if (subCategories == null || !subCategories.Any())
            {
                return ResultView<List<ProductCategoryDto>>.Failure("No subcategories found.");
            }

            var subCategoryDtos = _mapper.Map<List<ProductCategoryDto>>(subCategories);
            return ResultView<List<ProductCategoryDto>>.Success(subCategoryDtos);
        }

        public async Task<ResultView<IEnumerable<GetAllCategoriesDTO>>> GetSubCategoriesByMainCategoryIdAsync(int mainCategoryId)
        {
            var subCategories = await _repository.GetSubCategoriesByMainCategoryIdAsync(mainCategoryId);

            if (subCategories == null || !subCategories.Any())
            {
                return ResultView<IEnumerable<GetAllCategoriesDTO>>.Failure("No subcategories found for the specified main category ID.");
            }

            var mappedSubCategories = _mapper.Map<IEnumerable<GetAllCategoriesDTO>>(subCategories);
            return ResultView<IEnumerable<GetAllCategoriesDTO>>.Success(mappedSubCategories);
        }

        public async Task<ResultView<List<ProductCategoryDto>>> GetCategoriesByProductIdAsync(int productId)
        {
            var categories = await _repository.GetCategoriesByProductIdAsync(productId);

            if (!categories.Any())
            {
                return ResultView<List<ProductCategoryDto>>.Failure("No main categories found.");
            }

            var CategoryDtos = categories.Select(pc => _mapper.Map<ProductCategoryDto>(pc)).ToList();

            return ResultView<List<ProductCategoryDto>>.Success(CategoryDtos);
        }


    }
}
