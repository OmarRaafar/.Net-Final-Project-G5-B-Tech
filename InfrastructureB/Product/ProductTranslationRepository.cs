using ApplicationB.Contracts_B.Product;
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
    public class ProductTranslationRepository : GenericRepositoryB<ProductTranslationB>, IProductTranslationRepository
    {
        public ProductTranslationRepository(BTechDbContext context) : base(context) { }

        public IQueryable<ProductTranslationB> GetTranslationsByProductId(int productId)
        {
            return GetAll().Where(trans => trans.ProductId == productId);
        }
    }
}
