namespace TestPlatform.DTOs.BindingModels.Rooms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Attributes;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Services.Mapper.Interfaces;

    public class CreateRoomBM : IMapTo<Room>, IMapFrom<Room>
    {
        public CreateRoomBM()
        {
            var startDate = DateTime.Now.AddDays(1);
            var endDate = startDate.AddHours(1);

            this.StartDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, 0);
            this.EndDateTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 0);
        }

        [Required]
        [DisplayName("Start Date Time")]
        public DateTime StartDateTime { get; set; }

        [Required]
        [DisplayName("End Date Time")]
        [DateComparison(nameof(StartDateTime))]
        public DateTime EndDateTime { get; set; }

        public Guid TestId { get; set; }

        [DisplayName("Test Title")]
        public string TestTitle { get; set; }

        [DisplayName("Participants")]
        public IEnumerable<Guid> ParticipantsIds { get; set; }
    }
}
