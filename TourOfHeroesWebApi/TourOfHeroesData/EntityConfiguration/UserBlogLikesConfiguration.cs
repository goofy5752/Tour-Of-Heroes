namespace TourOfHeroesData.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    internal class UserBlogLikesConfiguration : IEntityTypeConfiguration<UserBlogLikes>
    {
        public void Configure(EntityTypeBuilder<UserBlogLikes> builder)
        {
            builder.HasKey(e => new { e.UserId, e.BlogId });

            builder.HasOne(e => e.User)
                .WithMany(c => c.UserBlogLikes)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Blog)
                .WithMany(c => c.BlogUserLikes)
                .HasForeignKey(e => e.BlogId);
        }
    }
}