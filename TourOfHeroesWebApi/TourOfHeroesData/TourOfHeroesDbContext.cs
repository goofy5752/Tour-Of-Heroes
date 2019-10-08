namespace TourOfHeroesData
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class TourOfHeroesDbContext : DbContext
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public TourOfHeroesDbContext(DbContextOptions<TourOfHeroesDbContext> options)
            : base(options) { }

        public DbSet<Hero> Heroes { get; set; }

        public DbSet<EditHistory> EditHistory { get; set; }
    }
}