namespace TestPlatform.Database.EntityTypeConfigurations.Rooms
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using TestPlatform.Database.Entities.Rooms;

    internal class RoomETC : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder
                .HasOne(r => r.Test)
                .WithMany(t => t.Rooms)
                .HasForeignKey(r => r.TestId);
        }
    }
}
