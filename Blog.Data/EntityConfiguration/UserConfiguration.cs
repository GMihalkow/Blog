using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .HasMany(u => u.Categories)
                .WithOne(c => c.Creator)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(u => u.Articles)
                .WithOne(a => a.Creator)
                .HasForeignKey(a => a.CreatorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}