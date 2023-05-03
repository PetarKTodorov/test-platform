namespace TestPlatform.Services.Database.Authorization
{
    using AutoMapper;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Authorization.Interfaces;

    public class UserRoleMapService : BaseService<UserRoleMap>, IUserRoleMapService
    {
        public UserRoleMapService(IBaseRepository<UserRoleMap> userRoleMapRepository, IMapper mapper)
            : base(userRoleMapRepository, mapper)
        {

        }
    }
}
