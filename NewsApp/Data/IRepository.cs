namespace NewsApp.Data
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);

        IQueryable<T> GetAll();
    }
}
