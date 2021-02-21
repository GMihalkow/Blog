using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.EntityConfiguration
{
    public class FaqQuestionConfiguration : IEntityTypeConfiguration<FaqQuestion>
    {
        public void Configure(EntityTypeBuilder<FaqQuestion> builder)
        {
            builder
                .HasKey(f => f.Id);

            builder
                .HasOne(f => f.Creator)
                .WithMany(u => u.Faqs)
                .HasForeignKey(f => f.CreatorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(f => f.Title)
                .HasMaxLength(500)
                .IsRequired();

            builder
                .Property(f => f.Title)
                .IsRequired();

            builder
                .Property(f => f.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}