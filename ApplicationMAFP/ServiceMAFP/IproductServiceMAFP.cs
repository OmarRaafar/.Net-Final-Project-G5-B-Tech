using DTOsMAFP.Product;
using ModelsMAFP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationMAFP.ServiceMAFP
{
    public interface IproductServiceMAFP: IGenericServiceMAFP<ProductMAFP>
    {
        public Task<List<GetProductDTO>> SearchByNameAsync(string name);

        ////public Task<EntityPaginated<GetBookDTO>> GetAllPaginatedAsync(int pageNumber, int Count);
        ////public Task<CreateOrUpdateBookDTO> GetIdAsync(int id);
    }
}
