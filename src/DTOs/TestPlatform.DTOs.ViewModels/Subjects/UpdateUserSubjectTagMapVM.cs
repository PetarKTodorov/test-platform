namespace TestPlatform.DTOs.ViewModels.Subjects
{
    using System;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UpdateUserSubjectTagMapVM : IMapFrom<UserSubjectTagMap>
    {
        public Guid Id { get; set; }

        public Guid SubjectTagId { get; set; }

        public string SubjectTagName { get; set; }
    }
}
