﻿namespace NewsApp.Data
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<IQueryable<T>> GetAllAsync();
    }
}
