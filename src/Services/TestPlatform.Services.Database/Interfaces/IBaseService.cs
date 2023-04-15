namespace TestPlatform.Services.Database.Interfaces
{
    using TestPlatform.Database.Entities;

    public interface IBaseService<TEntity>
        where TEntity : BaseEntity
    {
        Task<T> GetByIdAsync<T>(Guid id);

        Task<T> CreateAsync<T, TBindingModel>(TBindingModel model);

        Task<T> UpdateAsync<T, TBindingModel>(Guid id, TBindingModel model);

        Task<T> DeleteAsync<T>(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
