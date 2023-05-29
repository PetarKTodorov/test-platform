namespace TestPlatform.DTOs.BindingModels.Tests
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateTestBM : IMapTo<Test>
    {
        public CreateTestBM()
        {
            this.SubjectTagsIds = new List<Guid>();
        }

        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_EIGHT, MinimumLength = Validations.ONE)]
        public string Title { get; set; }

        [StringLength(maximumLength: Validations.TWO_POWER_SIXTEEN, MinimumLength = Validations.ONE)]
        public string Instructions { get; set; }

        [Required]
        public Guid StatusId { get; set; }

        [Required]
        [DisplayName("Has Randomize Questions")]
        public bool HasRandomizeQuestions { get; set; }

        [DisplayName("Subject Tags")]
        public IEnumerable<Guid> SubjectTagsIds { get; set; }
    }
}
