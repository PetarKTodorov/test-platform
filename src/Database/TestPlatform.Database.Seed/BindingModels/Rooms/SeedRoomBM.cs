namespace TestPlatform.Database.Seed.BindingModels.Rooms
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Attributes;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Mapper.Interfaces;

    public class SeedRoomBM : IMapTo<Room>
    {
        public Guid Id { get; set; }

        public Guid? CreatedBy { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        [DateComparison(nameof(StartDateTime))]
        public DateTime EndDateTime { get; set; }

        public Guid TestId { get; set; }
    }
}
