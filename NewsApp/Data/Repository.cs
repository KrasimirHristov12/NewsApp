using Microsoft.EntityFrameworkCore;

namespace NewsApp.Data
{
    public class Repository : IRepository
    {

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected ApplicationDbContext DbContext { get; set; }

        protected DbSet<T> DbSet<T>() where T: class
        {
            return DbContext.Set<T>();
        }
        public async Task AddAsync<T>(T entity) where T : class
        {
            await DbSet<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }



        public async Task DeleteAsync<T>(Guid id) where T : class
        {
            T entity = DbSet<T>().Find(id);

            if (entity != null)
            {
                DbSet<T>().Remove(entity);
                await DbContext.SaveChangesAsync();
            }
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return DbSet<T>().AsQueryable();
        }

        public async Task<T> GetByIdAsync<T>(string id) where T: class
        {
            if (!Guid.TryParse(id, out Guid identifier))
            {
                return null;
            }
             return await DbSet<T>().FindAsync(identifier);
            
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            DbSet<T>().Update(entity);
            await DbContext.SaveChangesAsync();
        }


    }
}
