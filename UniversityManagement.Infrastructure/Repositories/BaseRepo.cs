using Microsoft.EntityFrameworkCore;
using UniversityManagement.Core.Repositories;
using UniversityManagement.Infrastructure.Data;

namespace UniversityManagement.Infrastructure.Repositories
{
    public class BaseRepo<T> : IRepo<T> where T : class
    {

        protected readonly UniverisityDbContext DbContext;
        protected readonly DbSet<T> DbSet;
        public BaseRepo(UniverisityDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            DbSet.Add(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string user)
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
