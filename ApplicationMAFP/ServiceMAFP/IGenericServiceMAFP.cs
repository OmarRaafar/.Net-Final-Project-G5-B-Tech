using DTOsMAFP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationMAFP.ServiceMAFP
{
    public interface IGenericServiceMAFP<T>
    {
        public Task<ResultViewMAFP<T>> CreateAsync(T entity);

        public Task<ResultViewMAFP<T>> UpdateAsync(T entity);

        public Task<ResultViewMAFP<T>> DeleteAsync(T entity);

        public Task<T> GetByIDAsync(int id);

        public Task<List<T>> GetAllAsync();

        public Task<int> SaveChangesAsync();
    }
}
