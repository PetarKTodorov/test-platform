namespace TestPlatform.Database.EntityTypeConfigurations.Rooms
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TestPlatform.Database.Entities.Rooms;

    internal class ChatConnectionETC : IEntityTypeConfiguration<ChatConnection>
    {
        public void Configure(EntityTypeBuilder<ChatConnection> builder)
        {
            builder
                .HasOne(cc => cc.Room)
                .WithOne(u => u.ChatConnetion)
                .HasForeignKey<Room>(r => r.ChatConnectionId);
        }
    }
}
