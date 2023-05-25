﻿namespace TestPlatform.Services.Database.Authorization
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using TestPlatform.Common.Helpers;
    using TestPlatform.Database.Entities;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Mapper;

    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseRepository<User> userRepository, IMapper mapper)
            : base(userRepository, mapper)
        {

        }

        public override async Task<T> CreateAsync<T, TBindingModel>(TBindingModel model, Guid currentUserId)
        {
            BaseEntity currentUser = await this.FindByIdAsync<BaseEntity>(currentUserId);

            User entity = this.Mapper.Map<User>(model);
            entity.CreatedBy = currentUser.Id;
            entity.Password = PasswordHasher.HashPassword(entity.Password);

            entity = await this.BaseRepository.AddAsync(entity);
            await this.BaseRepository.SaveChangesAsync();

            T entityToReturn = this.Mapper.Map<T>(entity);

            return entityToReturn;
        }

        public async Task<T> FindByEmailAndPasswordAsync<T>(string email, string password)
        {
            User entity = await this.FindByEmailAsync<User>(email);

            if (entity == null)
            {
                return default(T);
            }

            bool isUserPassword = PasswordHasher.VerifyPassword(password, entity.Password);

            if (isUserPassword == false)
            {
                return default(T);
            }

            T entityToReturn = this.Mapper.Map<T>(entity);

            return entityToReturn;
        }

        public async Task<T> FindByEmailAsync<T>(string email)
        {
            User entity = await this.BaseRepository
                .GetAllAsQueryable()
                .SingleOrDefaultAsync(u => u.Email == email);

            T entityToReturn = this.Mapper.Map<T>(entity);

            return entityToReturn;
        }

        public async Task<T> FindUserRolesAsync<T>(Guid userId)
        {
            var user = await this.BaseRepository
                .GetByIdAsQueryable(userId)
                .To<T>()
                .SingleOrDefaultAsync();

            return user;
        }
    }
}
