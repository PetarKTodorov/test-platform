namespace TestPlatform.DTOs.ViewModels.Subjects
{
    using System.ComponentModel;

    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ListSubjectTagsVM : IMapFrom<SubjectTag>
    {
        public Guid Id { get; set; }

        [CustomSearchField]
        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        [CustomSearchField]
        public string Name { get; set; }
    }
}
