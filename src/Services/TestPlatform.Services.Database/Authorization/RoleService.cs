namespace TestPlatform.Services.Database.Authorization
{
    using AutoMapper;

    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Authorization.Interfaces;

    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IBaseRepository<Role> roleRepository, IMapper mapper)
            : base(roleRepository, mapper)
        {

        }
    }
}
