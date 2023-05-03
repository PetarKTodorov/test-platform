namespace TestPlatform.Database.EntityTypeConfigurations.Rooms
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Rooms;

    internal class RoomParticipantMapETC : IEntityTypeConfiguration<RoomParticipantMap>
    {
        public void Configure(EntityTypeBuilder<RoomParticipantMap> builder)
        {
            builder
                .HasAlternateKey(rpm => new { rpm.RoomId, rpm.UserId });

            builder
                .HasOne(rpm => rpm.Room)
                .WithMany(r => r.Participants)
                .HasForeignKey(rpm => rpm.RoomId);

            builder
                .HasOne(rpm => rpm.User)
                .WithMany(u => u.Rooms)
                .HasForeignKey(rpm => rpm.UserId);
        }
    }
}
