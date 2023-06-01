namespace TestPlatform.Services.Database.Authorization
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using TestPlatform.Common.Helpers;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Database.Subjects.Interfaces;
    using TestPlatform.Services.Mapper;

    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserSubjectTagMapService userSubjectTagMapService;

        public UserService(IBaseRepository<User> userRepository,
            IMapper mapper,
            IUserSubjectTagMapService userSubjectTagMapService)
            : base(userRepository, mapper)
        {
            this.userSubjectTagMapService = userSubjectTagMapService;
        }

        public override async Task<T> DeleteAsync<T>(Guid id, Guid currentUserId)
        {
            var resultFromDelete = await base.DeleteAsync<T>(id, currentUserId);

            await this.HardDeleteUserSubjectTagsMapAsync(id);

            return resultFromDelete;

        }

        public override async Task<T> CreateAsync<T, TBindingModel>(TBindingModel model, Guid currentUserId)
        {
            User entity = this.Mapper.Map<User>(model);
            entity.CreatedBy = currentUserId;
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
                .Where(u => u.IsDeleted == false)
                .SingleOrDefaultAsync(u => u.Email == email);

            T entityToReturn = this.Mapper.Map<T>(entity);

            return entityToReturn;
        }

        public IQueryable<T> FindAllUsersAsQueryable<T>()
        {
            return this.FindAllAsQueryable()
                .To<T>();
        }
        
        public async Task<T> FindAllByRoleIdAsync<T>(Guid roleId)
        {
            var entities = await this.BaseRepository
                .GetAllAsQueryable()
                .Where(u => u.Roles.Any(r => r.RoleId == roleId))
                .ToListAsync();

            T entityToReturn = this.Mapper.Map<T>(entities);

            return entityToReturn;
        }

        private async Task HardDeleteUserSubjectTagsMapAsync(Guid userId)
        {
            var userSubjectTags = await this.userSubjectTagMapService.FindUserSubjectTagsAsync<UserSubjectTagMap>(userId);

            foreach (var userSubjectTag in userSubjectTags)
            {
                await this.userSubjectTagMapService.HardDeleteAsync<UserSubjectTagMap>(userSubjectTag.Id);
            }
        }
    }
}
