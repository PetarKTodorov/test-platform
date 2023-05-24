namespace TestPlatform.Database.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Entities;

    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAllAsQueryable();

        Task<IEnumerable<TEntity>> GetAllAsync(bool isDeletedFlag = false);

        IQueryable<TEntity> GetByIdAsQueryable(Guid id, bool isDeletedFlag = false);

        Task<TEntity> GetByIdAsync(Guid id, bool isDeletedFlag = false);

        Task<TEntity> AddAsync(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity HardDelete(TEntity entity);

        TEntity Delete(TEntity entity);

        TEntity Restore(TEntity entity);

        Task<int> SaveChangesAsync();

        void DetachLocal(TEntity t, Guid entryId);
    }
}
