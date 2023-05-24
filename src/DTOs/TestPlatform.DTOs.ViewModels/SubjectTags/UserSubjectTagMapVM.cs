namespace TestPlatform.DTOs.ViewModels.SubjectTags
{
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UserSubjectTagMapVM : IMapFrom<UserSubjectTagMap>
    {
        public string UserEmail { get; set; }
    }
}
