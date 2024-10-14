using ApplicationB.Contracts_B;
using DbContextB;
using InfrastructureB.General;
using Microsoft.EntityFrameworkCore;
using ModelsB.Product_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureB.Product
{
    public class ProductRepository : GenericRepositoryB<ProductB>, IProductRepository
    {


        public ProductRepository(BTechDbContext context) : base(context)
        {
            {

            }
        }

        public IQueryable<ProductB> SearchByName(string name)
        {
            return GetAll().Where(p => p.Translations.Any(t => t.Name.Contains(name)));
        }

        ///// <summary>
        ///// Product
        ///// </summary>
        //public async Task<ProductB> GetByIdAsync(int id)
        //{
        //    return await _context.Products
        //        .Include(p => p.Images)
        //        .Include(p => p.Translations)
        //        .Include(p => p.Specifications) // Include specifications
        //        .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        //}

        //public IQueryable<ProductB> GetAllAsync()
        //{
        //    return _context.Products
        //        .Include(p => p.Images)
        //        .Include(p => p.Translations)
        //        .Where(p => !p.IsDeleted);
        //}

        //public IQueryable<ProductB> SearchByName(string name)
        //{
        //    return _context.Products
        //        .Include(p => p.Images)
        //        .Include(p => p.Translations)
        //        .Where(p => !p.IsDeleted &&
        //                     (p.Translations.Any(t => t.Name.Contains(name)) ||
        //                      p.Translations.Any(t => t.BrandName.Contains(name))));
        //}

        //public async Task AddAsync(ProductB product)
        //{
        //    await _context.Products.AddAsync(product);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdateAsync(ProductB product)
        //{
        //    _context.Products.Update(product);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var product = await GetByIdAsync(id);
        //    if (product != null)
        //    {
        //        product.IsDeleted = true;
        //        await UpdateAsync(product);
        //    }
        //}


        ///// <summary>
        ///// Image
        ///// </summary>


        //public async Task AddImagesAsync(int productId, IEnumerable<ProductImageB> images)
        //{
        //    var product = await GetByIdAsync(productId);
        //    if (product != null)
        //    {
        //        foreach (var image in images)
        //        {
        //            image.ProductId = productId; // Set the foreign key
        //            await _context.ProductImages.AddAsync(image);
        //        }
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task RemoveImageAsync(int productId, int imageId)
        //{
        //    var image = await _context.ProductImages.FindAsync(imageId);
        //    if (image != null && image.ProductId == productId)
        //    {
        //        _context.ProductImages.Remove(image);
        //        await _context.SaveChangesAsync();
        //    }
        //}


        ///// <summary>
        ///// Translation
        ///// </summary>
        //public async Task AddTranslationsAsync(int productId, IEnumerable<ProductTranslationB> translations)
        //{
        //    var product = await GetByIdAsync(productId);
        //    if (product != null)
        //    {
        //        foreach (var translation in translations)
        //        {
        //            translation.ProductId = productId; // Set the foreign key
        //            await _context.ProductTranslations.AddAsync(translation);
        //        }
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task RemoveTranslationAsync(int productId, int translationId)
        //{
        //    var translation = await _context.ProductTranslations.FindAsync(translationId);
        //    if (translation != null && translation.ProductId == productId)
        //    {
        //        _context.ProductTranslations.Remove(translation);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        ///// <summary>
        ///// Specification
        ///// </summary>

        //public async Task AddSpecificationsAsync(int productId, IEnumerable<ProductSpecificationsB> specifications)
        //{
        //    var product = await GetByIdAsync(productId);
        //    if (product != null)
        //    {
        //        foreach (var specification in specifications)
        //        {
        //            specification.ProductId = productId; // Set the foreign key
        //            await _context.ProductSpecifications.AddAsync(specification);
        //        }
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task RemoveSpecificationAsync(int productId, int specificationId)
        //{
        //    var specification = await _context.ProductSpecifications.FindAsync(specificationId);
        //    if (specification != null && specification.ProductId == productId)
        //    {
        //        _context.ProductSpecifications.Remove(specification);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public IQueryable<ProductSpecificationsB> GetSpecificationsByProductId(int productId)
        //{
        //    return _context.ProductSpecifications
        //        .Where(s => s.ProductId == productId);
        //    //}
        //}

    }
}