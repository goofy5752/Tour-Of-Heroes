using Microsoft.EntityFrameworkCore;
using TourOfHeroesData.Models;

namespace TourOfHeroesData
{
    public class TourOfHeroesDbContext : DbContext
    {
        public TourOfHeroesDbContext(DbContextOptions<TourOfHeroesDbContext> options)
            : base(options) { }

        public DbSet<Hero> Heroes { get; set; }

        public DbSet<EditHistory> EditHistory { get; set; }
    }
}
