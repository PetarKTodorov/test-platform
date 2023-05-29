namespace TestPlatform.Services.Database.Authorization
{
    using System.Linq;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.DTOs.BindingModels.User;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Mapper;

    public class UserRoleMapService : BaseService<UserRoleMap>, IUserRoleMapService
    {
        public UserRoleMapService(IBaseRepository<UserRoleMap> userRoleMapRepository, IMapper mapper)
            : base(userRoleMapRepository, mapper)
        {

        }

        public async Task<IEnumerable<T>> FindUserRolesAsync<T>(Guid userId)
        {
            var userSubjectTags = await this.FindAllAsQueryable<UserRoleMap>()
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToArrayAsync();

            return userSubjectTags;
        }

        public async Task UpdateUserRolesAsync(Guid userId, IEnumerable<Guid> userRoles, Guid currentUserId)
        {
            await this.AddRolesToUserAsync(userId, userRoles, currentUserId);
            await this.RemoveRolesFromUserAsync(userId, userRoles);
        }

        public async Task RemoveRoleFromUserAsync(Guid userRoleMapId)
        {
            await this.HardDeleteAsync<CreateUserRoleMap>(userRoleMapId);
        }

        public async Task AddRoleToUserAsync(Guid userId, Guid roleId, Guid currentUserId)
        {
            var userRoleMap = new CreateUserRoleMap()
            {
                UserId = userId,
                RoleId = roleId,
            };

            await this.CreateAsync<CreateUserRoleMap, CreateUserRoleMap>(userRoleMap, currentUserId);
        }

        private async Task AddRolesToUserAsync(Guid userId, IEnumerable<Guid> newRoles, Guid currentUserId)
        {
            var userWithRoles = await this.FindUserRolesAsync<UserRoleMap>(userId);
            var oldUserRolesIds = userWithRoles.Select(r => r.RoleId);

            var rolesToAdd = newRoles.Except(oldUserRolesIds);
            foreach (var roleId in rolesToAdd)
            {
                await this.AddRoleToUserAsync(userId, roleId, currentUserId);
            }
        }

        private async Task RemoveRolesFromUserAsync(Guid userId, IEnumerable<Guid> newRoles)
        {
            var userWithRoles = await this.FindUserRolesAsync<UserRoleMap>(userId);
            var oldUserRolesIds = userWithRoles.Select(r => r.RoleId);

            var rolesToRemove = oldUserRolesIds.Except(newRoles);
            foreach (var roleId in rolesToRemove)
            {
                var userRoleMapId = userWithRoles.First(ur => ur.RoleId == roleId);

                await this.RemoveRoleFromUserAsync(userRoleMapId.Id);
            }
        }
    }
}
