namespace TestPlatform.Services.Database.Authorization
{
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    using AutoMapper;

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

    public static class PasswordHasher
    {
        private const int ITERATIONS = 10000;
        private const int SALT_LENGHT = 16;
        private const int HASH_LENGTH = 32;

        public static string HashPassword(string password)
        {
            // Generate a random salt value.
            byte[] salt = new byte[SALT_LENGHT];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password using PBKDF2.
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONS);
            byte[] hash = pbkdf2.GetBytes(HASH_LENGTH);

            // Combine the salt and hash values into a single string for storage.
            byte[] hashBytes = new byte[SALT_LENGHT + HASH_LENGTH];
            Array.Copy(salt, 0, hashBytes, 0, SALT_LENGHT);
            Array.Copy(hash, 0, hashBytes, SALT_LENGHT, HASH_LENGTH);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }

        public static bool VerifyPassword(string password, string savedPasswordHash)
        {
            // Decode the saved password hash from its base64 representation.
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            // Extract the salt and hash values from the saved password hash.
            byte[] salt = new byte[SALT_LENGHT];
            Array.Copy(hashBytes, 0, salt, 0, SALT_LENGHT);
            byte[] hash = new byte[HASH_LENGTH];
            Array.Copy(hashBytes, SALT_LENGHT, hash, 0, HASH_LENGTH);

            // Compute the hash of the password using the extracted salt and the same number of iterations as when the password was hashed.
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONS);
            byte[] newHash = pbkdf2.GetBytes(HASH_LENGTH);

            // Compare the computed hash with the saved hash to see if the passwords match.
            for (int i = 0; i < HASH_LENGTH; i++)
            {
                if (hash[i] != newHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
