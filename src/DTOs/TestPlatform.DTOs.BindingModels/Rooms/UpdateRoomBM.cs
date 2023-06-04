namespace TestPlatform.DTOs.BindingModels.Rooms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    using AutoMapper;

    using TestPlatform.Common.Attributes;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Mapper.Interfaces;

    public class UpdateRoomBM : IMapTo<Room>, IMapFrom<Room>, IHaveCustomMappings
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        [DisplayName("Start Date Time")]
        public DateTime StartDateTime { get; set; }

        [Required]
        [DisplayName("End Date Time")]
        [DateComparison(nameof(StartDateTime))]
        public DateTime EndDateTime { get; set; }

        public Guid TestId { get; set; }

        public bool IsDeleted { get; set; }

        [DisplayName("Test Title")]
        public string TestTitle { get; set; }

        [DisplayName("Participants")]
        public IEnumerable<Guid> ParticipantsIds { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Room, UpdateRoomBM>()
                .ForMember(ctbm => ctbm.ParticipantsIds, mo => mo.MapFrom(t => t.Participants.Select(tsm => tsm.UserId)));
        }
    }
}
