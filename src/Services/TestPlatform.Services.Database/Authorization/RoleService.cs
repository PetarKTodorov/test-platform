namespace TestPlatform.Services.Database.Authorization
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using TestPlatform.Common.Constants;
    using TestPlatform.Common.Exceptions;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Mapper;

    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IBaseRepository<Role> roleRepository, IMapper mapper)
            : base(roleRepository, mapper)
        {

        }

        public async Task<T> FindByNameAsync<T>(string name)
        {
            Role entity = await this.BaseRepository.GetAllAsQueryable()
                .SingleOrDefaultAsync(r => r.Name == name);

            if (entity == null)
            {
                string message = string.Format(ExceptionMessages.ENTITY_NOT_FOUND, this.GetType().Name);
                throw new NotFoundException(message);
            }

            T model = this.Mapper.Map<T>(entity);

            return model;
        }
    }
}
