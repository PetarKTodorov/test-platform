namespace TestPlatform.DTOs.ViewModels.Subjects
{
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SubjectTagVm : IMapFrom<SubjectTag>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
