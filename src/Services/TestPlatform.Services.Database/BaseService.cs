namespace TestPlatform.Services.Database
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using TestPlatform.Common.Constants;
    using TestPlatform.Common.Exceptions;
    using TestPlatform.Database.Entities;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Interfaces;
    using TestPlatform.Services.Mapper;

    public abstract class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : BaseEntity
    {
        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            this.BaseRepository = baseRepository;
            this.Mapper = mapper;
        }

        protected virtual IMapper Mapper { get; }

        protected virtual IBaseRepository<TEntity> BaseRepository { get; set; }

        public virtual async Task<T> CreateAsync<T, TBindingModel>(TBindingModel model)
        {
            TEntity entity = this.Mapper.Map<TEntity>(model);

            entity = await this.BaseRepository.AddAsync(entity);
            await this.BaseRepository.SaveChangesAsync();

            T entityToReturn = this.Mapper.Map<T>(entity);

            return entityToReturn;
        }

        public virtual async Task<T> DeleteAsync<T>(Guid id)
        {
            TEntity entity = await this.FindByIdAsync<TEntity>(id);

            TEntity deletedEntity = this.BaseRepository.Delete(entity);
            await this.BaseRepository.SaveChangesAsync();

            T entityToReturn = this.Mapper.Map<T>(deletedEntity);

            return entityToReturn;
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync<T>(int page, int pageSize)
        {
            var colection = await this.BaseRepository.GetAllAsQueryable()
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .To<T>()
                .ToListAsync();

            return colection;
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync<T>(bool isDeletedFlag, int page, int pageSize)
        {
            var colection = await this.BaseRepository.GetAllAsQueryable()
                .Where(x => x.IsDeleted == isDeletedFlag)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .To<T>()
                .ToListAsync();

            return colection;
        }

        public virtual async Task<T> FindByIdAsync<T>(Guid id)
        {
            T entity = await this.BaseRepository.GetByIdAsQueryable(id)
                .To<T>()
                .SingleOrDefaultAsync();

            if (entity == null)
            {
                string message = string.Format(ExceptionMessages.ENTITY_NOT_FOUND, this.GetType().Name);
                throw new NotFoundException(message);
            }

            return entity;
        }

        public async Task<int> GetCountOfAllAsyns()
        {
            var countOfAllResults = await this.BaseRepository.GetAllAsQueryable()
                .CountAsync();

            return countOfAllResults;
        }

        public async Task<int> GetCountOfAllAsyns(bool isDeleted)
        {
            var collection = await this.BaseRepository.GetAllAsync(isDeleted);

            return collection.Count();
        }

        public virtual async Task<T> UpdateAsync<T, TBindingModel>(Guid id, TBindingModel model)
        {
            TEntity entity = await this.FindByIdAsync<TEntity>(id);

            TEntity updatedEntity = this.Mapper.Map(model, entity);

            updatedEntity = this.BaseRepository.Update(updatedEntity);
            await this.BaseRepository.SaveChangesAsync();

            T entityToReturn = this.Mapper.Map<T>(updatedEntity);

            return entityToReturn;
        }
    }
}
