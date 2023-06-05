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

        [DisplayName("Users with this subject tag")]
        public int UsersCount { get; set; }

        [DisplayName("Questions with this subject tag")]
        public int QuestionsCount { get; set; }

        [DisplayName("Tests with this subject tag")]
        public int TestsCount { get; set; }
    }
}
