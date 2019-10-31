namespace TourOfHeroesData
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Models;

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

    }
}