using ApplicationB.Contracts_B;
using ApplicationB.Contracts_B.Category;
using DbContextB;
using Microsoft.EntityFrameworkCore;
using ModelsB.Category_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace InfrastructureB.Category
{
    public class ProductCategoryRepository :IGenericRepositoryB<ProductCategoryB> ,IProductCategoryRepository
    {
        private readonly BTechDbContext _dbContext;

        public ProductCategoryRepository(BTechDbContext dbContextB) 
        {
            _dbContext = dbContextB;
        }

        public async Task AddAsync(ProductCategoryB productCategory)
        {
            await _dbContext.ProductCategories.AddAsync(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId, int categoryId)
        {
            var productCategory = await GetByIdAsync(productId, categoryId);
            if (productCategory != null)
            {
                _dbContext.ProductCategories.Remove(productCategory);
                await _dbContext.SaveChangesAsync();
            }
        }

       
        public async Task<bool> ExistsAsync(int productId, int categoryId)
        {
            return await _dbContext.ProductCategories.AnyAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);
        }

        public async Task<IEnumerable<ProductCategoryB>> GetAllAsync()
        {
            return await _dbContext.ProductCategories
                       .Include(pc => pc.Product)
                       .Include(pc => pc.Category)
                       .ToListAsync();
        }

        public async Task<ProductCategoryB> GetByIdAsync(int productId, int categoryId)
        {
            return await _dbContext.ProductCategories
                       .Include(pc => pc.Product)
                       .Include(pc => pc.Category)
                       .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);
        }

       
        public async Task UpdateAsync(ProductCategoryB productCategory)
        {
            _dbContext.ProductCategories.Update(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductCategoryB>> GetByCategoryNameAsync(string categoryName)
        {
            return await _dbContext.ProductCategories
                .Include(pc => pc.Product)
                .Include(pc => pc.Category)
                .Where(pc => pc.Category.Translations.Any(t => t.CategoryName.ToLower() == categoryName.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductCategoryB>> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.ProductCategories
                .Include(pc => pc.Product)
                .Include(pc => pc.Category)
                .Where(pc => pc.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductCategoryB>> GetMainCategoriesAsync()
        {
            return await _dbContext.ProductCategories
                .Include(pc => pc.Product)
                .Include(pc => pc.Category)
                .Where(pc => pc.IsMainCategory)
                .ToListAsync();
        }
        public async Task<List<ProductCategoryB>> GetSubCategoriesAsync()
        {
            return await _dbContext.ProductCategories
                .Include(pc => pc.Category)
                .Include(pc => pc.Product)
                .Where(pc => !pc.IsMainCategory)
                .ToListAsync();
        }
        public async Task<List<CategoryB>> GetSubCategoriesByMainCategoryIdAsync(int mainCategoryId)
        {
            return await _dbContext.ProductCategories
                   .Where(pc => !pc.IsMainCategory && pc.CategoryId == mainCategoryId) // Filter to get only subcategories
                   .Select(pc => pc.Category) // Select only the Category part
                   .ToListAsync(); // Convert to a list asynchronously
        }
       
        Task<IQueryable<ProductCategoryB>> IGenericRepositoryB<ProductCategoryB>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
         public Task<ProductCategoryB> GetByIdAsync(int id)
                {
                    throw new NotImplementedException();
                }
         public Task DeleteAsync(int id)
            {
                throw new NotImplementedException();
            }

    }
}
