namespace TestPlatform.Database.Seed.BindingModels.Subjects
{
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedCreateSubjectTagBM : IMapTo<SubjectTag>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Name { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }
    }
}
