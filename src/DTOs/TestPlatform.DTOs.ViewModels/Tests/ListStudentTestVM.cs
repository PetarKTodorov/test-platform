namespace TestPlatform.DTOs.ViewModels.Tests
{
    using AutoMapper;

    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ListStudentTestVM : IMapFrom<TestUserMap>, IHaveCustomMappings
    {
        public Guid TestId { get; set; }

        [CustomSearchField]
        public string Grade { get; set; }

        [CustomSearchField]
        public string TestTitle { get; set; }

        [CustomSearchField]
        public DateTime Date { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<TestUserMap, ListStudentTestVM>()
                .ForMember(ctbm => ctbm.Date, mo => mo.MapFrom(t => t.Test.Rooms.Where(x => x.TestId == t.TestId).Select(x => x.StartDateTime).First()));
        }
    }
}
