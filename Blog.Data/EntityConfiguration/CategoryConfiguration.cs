﻿using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasIndex(c => c.Name)
                .IsUnique();

            builder
                .Property(c => c.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .HasMany(c => c.Articles)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(c => c.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}