using System.Collections.Generic;
using System.Linq;
using TourOfHeroesData.Models;
using TourOfHeroesData.Seeder.Contracts;

namespace TourOfHeroesData.Seeder
{
    public class Seeder : ISeeder
    {
        private readonly TourOfHeroesDbContext _dbContext;

        public Seeder(TourOfHeroesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedDatabase()
        {
            var heroList = new List<Hero>();

            if (!_dbContext.Heroes.Any())
            {
                var cptAmerica = new Hero
                {
                    Name = "Captain America",
                    Description = "",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707816/HeroUploads/cpt_america_img_h3vbyt.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707816/HeroUploads/cpt_america_cover_c0v4og.png"
                };

                heroList.Add(cptAmerica);

                var spiderman = new Hero
                {
                    Name = "Spider-Man",
                    Description = "",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/spiderman-img_vcfskb.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/spiderman-cover_akceyp.jpg"
                };

                heroList.Add(spiderman);

                var thor = new Hero
                {
                    Name = "Thor",
                    Description = "",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707791/HeroUploads/thor-img_aeb3pa.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707792/HeroUploads/thor-cover_ab0mdt.jpg"
                };

                heroList.Add(thor);

                var hulk = new Hero
                {
                    Name = "Hulk",
                    Description = "",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707689/HeroUploads/hulk-img_dauwaa.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/hulk-cover_mgxrcq.jpg"
                };

                heroList.Add(hulk);

                var ironman = new Hero
                {
                    Name = "Iron Man",
                    Description = "",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707689/HeroUploads/ironman-img_kvxuvy.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707689/HeroUploads/ironman-cover_kvdzuy.jpg"
                };

                heroList.Add(ironman);

                var hawkeye = new Hero
                {
                    Name = "Hawkeye",
                    Description = "",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/hawkeye-img_lvvkth.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/hawkeye-cover_dpzgsd.jpg"
                };

                heroList.Add(hawkeye);

                _dbContext.Heroes.AddRange(heroList);
                _dbContext.SaveChangesAsync();
            }
        }
    }
}
