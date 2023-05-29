namespace TestPlatform.DTOs.BindingModels.Subjects
{
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateTestSubjectTagMapBM : IMapTo<TestSubjectTagMap>
    {
        [Required]
        public Guid TestId { get; set; }

        [Required]
        public Guid SubjectTagId { get; set; }
    }
}
