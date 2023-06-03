namespace TestPlatform.DTOs.ViewModels.Rooms
{
    using System;
    using System.ComponentModel;
    using AutoMapper;
    using TestPlatform.Application.Infrastructures.Filtres;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Mapper.Interfaces;

    public class ListStudentRoomVM : IMapFrom<Room>
    {
        public Guid Id { get; set; }

        [DisplayName("Start Date Time")]
        [CustomSearchField]
        public DateTime StartDateTime { get; set; }

        [DisplayName("End Date Time")]
        [CustomSearchField]
        public DateTime EndDateTime { get; set; }

        public Guid TestId { get; set; }

        [DisplayName("Test Title")]
        [CustomSearchField]
        public string TestTitle { get; set; }

        [DisplayName("Instructions")]
        public string TestInstructions { get; set; }

        public string Grade { get; set; }
    }
}
