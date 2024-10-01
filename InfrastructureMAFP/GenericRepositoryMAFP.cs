using ApplicationMAFP.ContractsMAFP;
using DbContextMAFP;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureMAFP
{
    public class GenericRepositoryMAFP<T>: IGenericRepositoryMAFP<T> where T : class
    {
        private readonly ASPDbContextMAFP context;
        private readonly DbSet<T> dbSet;

        public GenericRepositoryMAFP(ASPDbContextMAFP dbContext)
        {
            context = dbContext;
            dbSet = context.Set<T>();
        }
        public async Task<T> CreateAsync(T entity) => (await dbSet.AddAsync(entity)).Entity;

        public void DeleteAsync(T entity) => context.Remove(entity);


        public async Task<IQueryable<T>> GetAllAsync() => await Task.FromResult(dbSet.Select(b => b));



        public async Task<T> GetByIDAsync(int id) => await dbSet.FindAsync(id);


        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();

        public async Task<T> UpdateAsync(T entity) => await Task.FromResult(dbSet.Update(entity).Entity);
    }
}
