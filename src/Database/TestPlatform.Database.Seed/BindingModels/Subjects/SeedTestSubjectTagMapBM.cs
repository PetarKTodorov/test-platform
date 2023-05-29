namespace TestPlatform.Database.Seed.BindingModels.Subjects
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedTestSubjectTagMapBM : IMapTo<TestSubjectTagMap>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid TestId { get; set; }

        [Required]
        public Guid SubjectTagId { get; set; }
    }
}
