namespace TestPlatform.DTOs.BindingModels.User
{
    using System;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ProfileUserBM : IMapFrom<User>, IMapTo<User>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    }
}
