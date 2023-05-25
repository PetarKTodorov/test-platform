namespace TestPlatform.Database.Seed.BindingModels.Subjects
{
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedUserSubjectTagMapBM : IMapTo<UserSubjectTagMap>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid SubjectTagId { get; set; }
    }
}
