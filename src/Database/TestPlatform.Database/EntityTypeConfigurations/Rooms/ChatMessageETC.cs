namespace TestPlatform.Database.EntityTypeConfigurations.Rooms
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TestPlatform.Database.Entities.Rooms;

    internal class ChatMessageETC : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder
                .HasOne(cm => cm.Room)
                .WithMany(r => r.ChatMessages)
                .HasForeignKey(cm => cm.RoomId);

            builder
                .HasOne(cm => cm.User)
                .WithMany(u => u.ChatMessages)
                .HasForeignKey(cm => cm.UserId);
        }
    }
}
