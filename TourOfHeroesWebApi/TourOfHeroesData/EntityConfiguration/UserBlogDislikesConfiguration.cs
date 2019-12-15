namespace TourOfHeroesData.EntityConfiguration
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserBlogDislikesConfiguration : IEntityTypeConfiguration<UserBlogDislikes>
    {
        public void Configure(EntityTypeBuilder<UserBlogDislikes> builder)
        {
            builder.HasKey(e => new { e.UserId, e.BlogId });

            builder.HasOne(e => e.User)
                .WithMany(c => c.UserBlogDislikes)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Blog)
                .WithMany(c => c.BlogUserDislikes)
                .HasForeignKey(e => e.BlogId);
        }
    }
}
