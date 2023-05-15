namespace TestPlatform.DTOs.ViewModels.SubjectTags
{
    using System.ComponentModel;

    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class AllSubjectTagsVM : IMapFrom<SubjectTag>
    {
        public Guid Id { get; set; }

        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        public string Name { get; set; }
    }
}
