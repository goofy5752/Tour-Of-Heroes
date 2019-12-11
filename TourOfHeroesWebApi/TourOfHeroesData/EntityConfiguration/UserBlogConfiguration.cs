namespace TourOfHeroesData.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    internal class UserBlogConfiguration : IEntityTypeConfiguration<UserBlog>
    {
        public void Configure(EntityTypeBuilder<UserBlog> builder)
        {
            builder.HasKey(e => new { e.UserId, e.BlogId });

            builder.HasOne(e => e.User)
                .WithMany(c => c.UserBlogs)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Blog)
                .WithMany(c => c.BlogUsers)
                .HasForeignKey(e => e.BlogId);
        }
    }
}