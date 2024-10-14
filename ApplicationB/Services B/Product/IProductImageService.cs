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
    public interface IProductImageService
    {
        public  Task<ResultView<ProductImageDto>> AddImageAsync(ProductImageDto productImageDto, int userId);
        public Task<ResultView<ProductImageDto>> UpdateImageAsync(ProductImageDto productImageDto, int userId);
        public  Task<ResultView<ProductImageDto>> DeleteImageAsync(int id);
        public IQueryable<ProductImageB> GetImagesByProductId(int productId);
    }
}
