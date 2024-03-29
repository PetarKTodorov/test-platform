﻿namespace TestPlatform.Services.Database.Interfaces
{
    using TestPlatform.Database.Entities;

    public interface IBaseService<TEntity>
        where TEntity : BaseEntity
    {
        Task<T> FindByIdAsync<T>(Guid id);

        Task<T> FindByIdAsync<T>(Guid id, bool isDeletedFlag);

        Task<T> CreateAsync<T, TBindingModel>(TBindingModel model, Guid currentUserId);

        Task<T> UpdateAsync<T, TBindingModel>(Guid id, TBindingModel model, Guid currentUserId);

        Task<T> HardDeleteAsync<T>(Guid id);

        Task<T> DeleteAsync<T>(Guid id, Guid currentUserId);

        Task<T> RestoryAsync<T>(Guid id, Guid currentUserId);

        IQueryable<TEntity> FindAllAsQueryable();

        Task<IEnumerable<T>> FindAllAsync<T>();

        Task<IEnumerable<T>> FindAllAsync<T>(bool isDeletedFlag);
    }
}
