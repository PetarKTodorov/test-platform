namespace TestPlatform.DTOs.BindingModels.Subjects
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UpdateSubjectTagBM : IMapTo<SubjectTag>, IMapFrom<SubjectTag>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Name { get; set; }
    }
}
