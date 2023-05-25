namespace TestPlatform.DTOs.BindingModels.Subjects
{
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateSubjectTagBM : IMapTo<SubjectTag>
    {
        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Name { get; set; }
    }
}
