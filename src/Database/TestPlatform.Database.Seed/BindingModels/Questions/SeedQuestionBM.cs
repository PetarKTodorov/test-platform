namespace TestPlatform.Database.Seed.BindingModels.Questions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedQuestionBM : IMapTo<Question>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_SIXTEEN, MinimumLength = Validations.ONE)]
        public string Title { get; set; }
    }
}
