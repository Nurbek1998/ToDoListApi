using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoListApi.Entities.Configurations
{
    public class ToDosConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(1000);
            builder.Property(x => x.IsCompleted)
                .HasDefaultValue(false);
            builder.HasOne(x => x.User)
                .WithMany(t => t.ToDos)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
