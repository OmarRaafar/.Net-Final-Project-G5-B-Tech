using DbContextMAFP;
using ModelsMAFP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureMAFP
{
    public class ProductRepositoryMAFP: GenericRepositoryMAFP<ProductMAFP>
    {
        public ProductRepositoryMAFP(ASPDbContextMAFP dbContext): base(dbContext) 
        {
            
        }
    }
}
