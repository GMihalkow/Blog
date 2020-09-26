using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.EntityConfiguration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(a => a.Title)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(a => a.Content)
                .IsRequired(false);

            builder
                .Property(a => a.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}