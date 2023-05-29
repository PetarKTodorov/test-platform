namespace TestPlatform.DTOs.ViewModels.Tests
{
    using System;
    using System.ComponentModel;
    using AutoMapper;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ListPendingTestVM : IMapFrom<Test>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid StatusId { get; set; }

        public bool IsDeleted { get; set; }

        [CustomSearchField]
        public string Title { get; set; }

        [DisplayName("Approvers Count")]
        [CustomSearchField]
        public int ApproversCount { get; set; }

        public IEnumerable<Guid> ApproversIds { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Test, ListPendingTestVM>()
                .ForMember(lpt => lpt.ApproversIds,
                    mo => mo.MapFrom(t => t.Approvers.Select(tum => tum.UserId)));
        }
    }
}
