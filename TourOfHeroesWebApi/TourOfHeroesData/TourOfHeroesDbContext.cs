namespace TourOfHeroesData
{
    using EntityConfiguration;
    using Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class TourOfHeroesDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public TourOfHeroesDbContext(DbContextOptions<TourOfHeroesDbContext> options)
            : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<EditHistory> EditHistory { get; set; }

        public DbSet<Hero> Heroes { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<LikedMovie> LikedMovies { get; set; }

        public DbSet<UserBlogLikes> UserBlogLikes { get; set; }

        public DbSet<UserBlogDislikes> UserBlogDislikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserBlogLikesConfiguration());

            modelBuilder.ApplyConfiguration(new UserBlogDislikesConfiguration());
        }
    }
}