using Microsoft.EntityFrameworkCore;
using TourOfHeroesWebApi.Data.Models;

namespace TourOfHeroesWebApi.Data
{
    public class TourOfHeroesDbContext : DbContext
    {
        public TourOfHeroesDbContext() {}

        public TourOfHeroesDbContext(DbContextOptions options)
            :base(options) {}

        public DbSet<Hero> Heroes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-QASR8MP\\SQLEXPRESS;Database=TourOfHeroes;Integrated Security=True;");
        }
    }
}
