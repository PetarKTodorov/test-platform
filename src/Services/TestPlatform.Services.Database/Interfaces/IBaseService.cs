namespace TestPlatform.Services.Database.Interfaces
{
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities;

    public interface IBaseService<TEntity>
        where TEntity : BaseEntity
    {
        Task<T> FindByIdAsync<T>(Guid id);

        Task<T> CreateAsync<T, TBindingModel>(TBindingModel model);

        Task<T> UpdateAsync<T, TBindingModel>(Guid id, TBindingModel model);

        Task<T> DeleteAsync<T>(Guid id);

        Task<int> GetCountOfAllAsyns();

        Task<int> GetCountOfAllAsyns(bool isDeleted);

        IQueryable<T> FindAllAsQueryable<T>();

        Task<IEnumerable<T>> FindAllAsync<T>(int page, int pageSize);

        Task<IEnumerable<T>> FindAllAsync<T>(bool isDeletedFlag, int page, int pageSize);
    }
}
