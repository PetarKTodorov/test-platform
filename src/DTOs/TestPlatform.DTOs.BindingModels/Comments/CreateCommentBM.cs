namespace TestPlatform.DTOs.BindingModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Comments;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateCommentBM : BaseBM, IMapTo<TestComment>, IMapFrom<TestComment>
    {
        [Required]
        [StringLength(maximumLength: Validations.TWO_POWER_THIRTEEN, MinimumLength = Validations.ONE)]
        public string Content { get; set; }

        [Required]
        public Guid TestId { get; set; }

        public string UserEmail { get; set; }
    }
}
