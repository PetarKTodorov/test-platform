namespace TestPlatform.Database.Seed.BindingModels.Questions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedAnswerBM : IMapTo<Answer>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_SIXTEEN, MinimumLength = Validations.ONE)]
        public string Content { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
