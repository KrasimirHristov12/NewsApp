namespace NewsApp.Data
{
    public interface IRepository 
    {
        Task AddAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(Guid id) where T : class;

        Task<T> GetByIdAsync<T>(string id) where T: class;
        IQueryable<T> GetAll<T>() where T: class;
    }
}
