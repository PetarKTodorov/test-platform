namespace TestPlatform.Services.Database.Authorization
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

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
    }
}
