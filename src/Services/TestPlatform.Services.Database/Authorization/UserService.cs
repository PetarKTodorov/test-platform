namespace TestPlatform.Services.Database.Authorization
{
    using System.Threading.Tasks;

    using AutoMapper;

    using TestPlatform.Common.Helpers;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Repositories.Interfaces;
    using TestPlatform.Services.Database.Authorization.Interfaces;

    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseRepository<User> userRepository, IMapper mapper)
            : base(userRepository, mapper)
        {

        }

        public override async Task<T> CreateAsync<T, TBindingModel>(TBindingModel model)
        {
            User entity = this.Mapper.Map<User>(model);
            entity.Password = PasswordHasher.HashPassword(entity.Password);

            entity = await this.BaseRepository.AddAsync(entity);
            await this.BaseRepository.SaveChangesAsync();

            T entityToReturn = this.Mapper.Map<T>(entity);

            return entityToReturn;
        }
    }
}
