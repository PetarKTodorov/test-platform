﻿namespace TestPlatform.Services.Database
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

        public virtual async Task<T> CreateAsync<T, TBindingModel>(TBindingModel model, Guid currentUserId)
        {
            TEntity entity = this.Mapper.Map<TEntity>(model);
            entity.CreatedBy = currentUserId;

            this.BaseRepository.DetachLocal(entity);
            entity = await this.BaseRepository.AddAsync(entity);
            await this.BaseRepository.SaveChangesAsync();

            T entityToReturn = this.Mapper.Map<T>(entity);

            return entityToReturn;
        }

        public virtual async Task<T> HardDeleteAsync<T>(Guid id)
        {
            TEntity entity = await this.FindByIdAsync(id);

            this.BaseRepository.DetachLocal(entity);

            TEntity deletedEntity = this.BaseRepository.HardDelete(entity);
            await this.BaseRepository.SaveChangesAsync();

            T entityToReturn = this.Mapper.Map<T>(deletedEntity);

            return entityToReturn;
        }

        public virtual async Task<T> DeleteAsync<T>(Guid id, Guid currentUserId)
        {
            TEntity entity = await this.FindByIdAsync(id);
            entity.ModifiedBy = currentUserId;
            entity.DeletedBy = currentUserId;

            this.BaseRepository.DetachLocal(entity);
            TEntity deletedEntity = this.BaseRepository.Delete(entity);
            await this.BaseRepository.SaveChangesAsync();

            T entityToReturn = this.Mapper.Map<T>(deletedEntity);

            return entityToReturn;
        }

        public virtual async Task<T> RestoryAsync<T>(Guid id, Guid currentUserId)
        {
            TEntity entity = await this.FindByIdAsync(id, true);
            entity.ModifiedBy = currentUserId;

            this.BaseRepository.DetachLocal(entity);
            TEntity restoredEntity = this.BaseRepository.Restore(entity);
            await this.BaseRepository.SaveChangesAsync();

            T entityToReturn = this.Mapper.Map<T>(restoredEntity);

            return entityToReturn;
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync<T>()
        {
            var colection = await this.BaseRepository.GetAllAsQueryable()
                .To<T>()
                .ToListAsync();

            return colection;
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync<T>(bool isDeletedFlag)
        {
            var colection = await this.BaseRepository.GetAllAsQueryable()
                .Where(x => x.IsDeleted == isDeletedFlag)
                .To<T>()
                .ToListAsync();

            return colection;
        }

        public virtual IQueryable<TEntity> FindAllAsQueryable()
        {
            var colection = this.BaseRepository.GetAllAsQueryable();

            return colection;
        }

        public virtual async Task<T> FindByIdAsync<T>(Guid id)
        {
            var entity = await this.BaseRepository.GetByIdAsQueryable(id)
                .To<T>()
                .SingleOrDefaultAsync();

            var administratorId = new Guid(GlobalConstants.ADMINISTRATOR_ID);
            if (id == administratorId && entity == null)
            {
                var baseEntity = new BaseEntity()
                {
                    Id = administratorId,
                };

                entity = this.Mapper.Map<BaseEntity, T>(baseEntity);
            }
            else if (entity == null)
            {
                string message = string.Format(ExceptionMessages.ENTITY_NOT_FOUND, this.GetType().Name);
                throw new NotFoundException(message);
            }

            return entity;
        }

        public virtual async Task<T> FindByIdAsync<T>(Guid id, bool isDeletedFlag)
        {
            var entity = await this.BaseRepository.GetByIdAsQueryable(id, isDeletedFlag)
                .To<T>()
                .SingleOrDefaultAsync();

            if (entity == null)
            {
                string message = string.Format(ExceptionMessages.ENTITY_NOT_FOUND, this.GetType().Name);
                throw new NotFoundException(message);
            }

            return entity;
        }

        public virtual async Task<T> UpdateAsync<T, TBindingModel>(Guid id, TBindingModel model, Guid currentUserId)
        {
            TEntity entity = await this.FindByIdAsync(id);

            this.BaseRepository.DetachLocal(entity);

            TEntity updatedEntity = this.Mapper.Map(model, entity);
            updatedEntity.ModifiedBy = currentUserId;

            updatedEntity = this.BaseRepository.Update(updatedEntity);
            await this.BaseRepository.SaveChangesAsync();

            T entityToReturn = this.Mapper.Map<T>(updatedEntity);

            return entityToReturn;
        }

        private async Task<TEntity> FindByIdAsync(Guid id)
        {
            var entity = await this.BaseRepository.GetByIdAsQueryable(id)
                .SingleOrDefaultAsync();

            var administratorId = new Guid(GlobalConstants.ADMINISTRATOR_ID);
            if (id == administratorId && entity == null)
            {
                var baseEntity = new BaseEntity()
                {
                    Id = administratorId,
                };

                entity = this.Mapper.Map<BaseEntity, TEntity>(baseEntity);
            }
            else if (entity == null)
            {
                string message = string.Format(ExceptionMessages.ENTITY_NOT_FOUND, this.GetType().Name);
                throw new NotFoundException(message);
            }

            return entity;
        }

        private async Task<TEntity> FindByIdAsync(Guid id, bool isDeletedFlag)
        {
            var entity = await this.BaseRepository.GetByIdAsQueryable(id, isDeletedFlag)
                .SingleOrDefaultAsync();

            if (entity == null)
            {
                string message = string.Format(ExceptionMessages.ENTITY_NOT_FOUND, this.GetType().Name);
                throw new NotFoundException(message);
            }

            return entity;
        }
    }
}
