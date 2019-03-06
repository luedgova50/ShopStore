namespace ShopStore.WebMarket.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Entities;
    using Microsoft.EntityFrameworkCore;
    

    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContext context;

        public GenericRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.
                Set<T>().
                AsNoTracking().
                FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);

            await SaveAllAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            this.context.Set<T>().
                Update(entity);

            await SaveAllAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            this.context.Set<T>().
                Remove(entity);

            await SaveAllAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await context.
                Set<T>().
                AnyAsync(e => e.Id == id);

        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.
                SaveChangesAsync() > 0;
        }


    }
}
