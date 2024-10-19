using ApplicationB.Contracts_B.Category;
using ApplicationB.Services_B.General;
using AutoMapper;
using DTOsB.Category;
using Microsoft.AspNetCore.Http;
using ModelsB.Category_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILanguageService _languageService;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IUserService userService, ILanguageService languageService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _userService = userService;
            _languageService = languageService;
        }

        public async Task<IEnumerable<GetAllCategoriesDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var activeCategories = categories.Where(c => !c.IsDeleted);
            return _mapper.Map<IEnumerable<GetAllCategoriesDTO>>(activeCategories);

        }
        public async Task<CategoryB> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {

                throw new Exception($"Category with ID {id} not found.");
            }
            return (category);
        }
        public async Task<GetAllCategoriesDTO> GetCategoryByNameAsync(string categoryName)
        {
            var category = await _categoryRepository.GetByNameAsync(categoryName);
            if (category == null)
            {
                throw new Exception($"Category with Name {categoryName} not found.");

            }
            return _mapper.Map<GetAllCategoriesDTO>(category);
        }
        public async Task AddCategoryAsync(CreateOrUpdateCategoriesDTO createCategoryDto, IFormFile imageFile)
        {
            // Check if the category name already exists
            var existingCategory = await _categoryRepository
                .AnyAsync(c => c.Translations.Any(t => t.CategoryName.Equals(createCategoryDto.Translations.First().CategoryName, StringComparison.OrdinalIgnoreCase)));

            if (existingCategory)
            {
                throw new Exception("A category with the same name already exists.");
            }

            // Handle image upload (convert IFormFile to a URL or save path)
            string imageUrl = await HandleImageUploadAsync(imageFile);

            // Set user and language information
            var currentUserId = _userService.GetCurrentUserId();
            var currentLanguage = _languageService.GetCurrentLanguageCode();

            // Map the DTO to the entity
            var category = _mapper.Map<CategoryB>(createCategoryDto);
            category.CreatedBy = currentUserId;
            category.ImageUrl = imageUrl;

            //ismain
            // Add product category relationship (with IsMainCategory)
            foreach (var translation in createCategoryDto.Translations)
            {
                var productCategory = new ProductCategoryB
                {
                    Category = category,
                    IsMainCategory = translation.IsMainCategory
                };
                category.ProductCategories.Add(productCategory);
            }
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();


        }
        public async Task UpdateCategoryAsync(int id, CreateOrUpdateCategoriesDTO categoryDto, IFormFile imageFile)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            if (categoryEntity == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            // Handle image upload (convert IFormFile to a URL or save path)
            if (imageFile != null)
            {
                string imageUrl = await HandleImageUploadAsync(imageFile);
                categoryEntity.ImageUrl = imageUrl;
            }

            // Set updated user and updated date
            categoryEntity.UpdatedBy = _userService.GetCurrentUserId();
            categoryEntity.Updated = DateTime.Now;

            //IsMain
            foreach (var translation in categoryDto.Translations)
            {

                var productCategory = categoryEntity.ProductCategories
                    .FirstOrDefault(pc => pc.CategoryId == categoryEntity.Id);

                if (productCategory != null)
                {
                    productCategory.IsMainCategory = translation.IsMainCategory; 
                }
                else
                {
                    categoryEntity.ProductCategories.Add(new ProductCategoryB
                    {
                        CategoryId = categoryEntity.Id,
                        IsMainCategory = translation.IsMainCategory
                    });
                }
            }
            _mapper.Map(categoryDto, categoryEntity);
                await _categoryRepository.UpdateAsync(categoryEntity);

                // save changes
                await _categoryRepository.SaveChangesAsync();
            }
        
            public async Task DeleteCategoryAsync(int id)
            {
                var categoryEntity = await _categoryRepository.GetByIdAsync(id);
                if (categoryEntity == null)
                {
                    throw new KeyNotFoundException($"Category with ID {id} not found.");
                }
                 // Check if the category is linked to any products or subcategories
                 if (categoryEntity.ProductCategories.Any())
                 {
                   // You can choose to throw an exception, log a warning, or handle it accordingly
                   throw new InvalidOperationException($"Category with ID {id} cannot be deleted because it has associated products or subcategories.");
                  }
            // soft delete
            categoryEntity.IsDeleted = true;

                await _categoryRepository.UpdateAsync(categoryEntity);
                await _categoryRepository.SaveChangesAsync();
            }

            public async Task<string> HandleImageUploadAsync(IFormFile imageFile)
            {
                if (imageFile == null || imageFile.Length == 0)
                    return null;

                // Logic to save the image to a folder and return the file path or URL
                var fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                var fileExtension = Path.GetExtension(imageFile.FileName);
                var newFileName = $"{fileName}_{DateTime.Now.Ticks}{fileExtension}";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImageUrls/categories", newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return $"/images/categories/{newFileName}"; // Return the relative URL or path        }
            }
        }
    } 

