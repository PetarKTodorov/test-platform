namespace TestPlatform.DTOs.ViewModels.Subjects
{
    using System.ComponentModel;

    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class DetailsSubjectTagVM : IMapFrom<SubjectTag>
    {
        public Guid Id { get; set; }

        [DisplayName("Created By")]
        public Guid CreatedBy { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Modified By")]
        public Guid? ModifiedBy { get; set; }

        [DisplayName("Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [DisplayName("Deleted By")]
        public Guid? DeletedBy { get; set; }

        [DisplayName("Deleted Date")]
        public DateTime? DeletedDate { get; set; }

        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        public IEnumerable<UserSubjectTagMapVM> Users { get; set; }
    }
}
