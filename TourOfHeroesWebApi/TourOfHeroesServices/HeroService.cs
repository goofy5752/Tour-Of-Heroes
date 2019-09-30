using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourOfHeroesData.Common.Contracts;
using TourOfHeroesData.Models;
using TourOfHeroesDTOs;
using TourOfHeroesServices.Contracts;

namespace TourOfHeroesServices
{
    public class HeroService : IHeroService
    {
        private readonly IRepository<Hero> _heroRepository;
        private readonly IImageService _imageService;

        public HeroService(IRepository<Hero> heroRepository, IImageService imageService)
        {
            _heroRepository = heroRepository;
            _imageService = imageService;
        }

        public IEnumerable<Hero> GetAllHeroes()
        {
            var allHeroes = _heroRepository
                .All()
                .ToList();

            return allHeroes;
        }

        public Hero GetById(int id)
        {
            var hero = this._heroRepository
                .All()
                .Single(x => x.Id == id);

            return hero;
        }

        public IEnumerable<Hero> GetHeroBySearchString(string name)
        {
            return this._heroRepository.All().Where(x => x.Name.Contains(name));
        }

        public async Task CreateHero(CreateHeroDTO hero)
        {
            var imgUrl = this._imageService.AddToCloudinaryAndReturnImageUrl(hero.Image);
            var coverImgUrl = this._imageService.AddToCloudinaryAndReturnImageUrl(hero.CoverImage);
            await this._imageService.SaveAllAsync();
            var heroObj = new Hero
            {
                Name = hero.Name,
                Description = hero.Description,
                Image = imgUrl,
                CoverImage = coverImgUrl,
                RealName = hero.RealName,
                Birthday = hero.Birthday.Date,
                Gender = hero.Gender
            };

            await this._heroRepository.AddAsync(heroObj);

            await this._heroRepository.SaveChangesAsync();
        }

        public async Task UpdateHero(int id, Hero hero)
        {
            var dbHero = this._heroRepository.All().FirstOrDefault(x => x.Id == id);

            var editHistory = new EditHistory()
            {
                OldValue = dbHero.Name,
                NewValue = hero.Name,
                HeroId = dbHero.Id
            };
            dbHero.EditHistory.Add(editHistory);
            dbHero.Name = hero.Name;

            await this._heroRepository.SaveChangesAsync();
        }

        public async Task DeleteHero(int id)
        {
            var heroToDelete = this._heroRepository.All().FirstOrDefault(x => x.Id == id);

            this._heroRepository.Delete(heroToDelete);

            await this._heroRepository.SaveChangesAsync();
        }
    }
}
