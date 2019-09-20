using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourOfHeroesData;
using TourOfHeroesData.Models;
using TourOfHeroesServices.Contracts;
using TourOfHeroesWebApi.DTOs;

namespace TourOfHeroesWebApi.Controllers
{
    public class HeroesController : ApiController
    {
        private readonly TourOfHeroesDbContext _dbContext;
        private readonly IImageService _imageService;

        public HeroesController(TourOfHeroesDbContext dbContext, IImageService imageService)
        {
            _dbContext = dbContext;
            _imageService = imageService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Hero>> Get()
        {
            return this._dbContext.Heroes.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Hero> Get(int id)
        {
            var hero = this._dbContext.Heroes.FirstOrDefault(x => x.Id == id);
            return hero;
        }

        [HttpGet]
        [Route("get-heroes")]
        public ActionResult<IEnumerable<Hero>> GetHeroesBySearchString(string name)
        {
            var heroes = this._dbContext.Heroes.Where(x => x.Name.Contains(name)).ToList();
            if (heroes.Count != 0)
            {
                return heroes;
            }
            else
            {
                return this.NoContent();
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<Hero>> Post(CreateHeroDTO hero)
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
                Birthday = hero.Birthday,
                Gender = hero.Gender
            };

            await this._dbContext.Heroes.AddAsync(heroObj);
            await this._dbContext.SaveChangesAsync();
            return this.CreatedAtAction("Get", new { id = heroObj.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Hero hero)
        {
            var dbHero = this._dbContext.Heroes.FirstOrDefault(x => x.Id == id);
            if (dbHero != null)
            {
                var editHistory = new EditHistory()
                {
                    OldValue = dbHero.Name,
                    NewValue = hero.Name,
                    Hero = hero
                };
                dbHero.EditHistories.Add(editHistory);
            }

            if (dbHero != null) dbHero.Name = hero.Name;
            await _dbContext.SaveChangesAsync();
            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Hero>> Delete(int id)
        {
            var hero = this._dbContext.Heroes.FirstOrDefault(x => x.Id == id);
            if (hero == null)
            {
                return this.NotFound();
            }

            this._dbContext.Heroes.Remove(hero);
            await this._dbContext.SaveChangesAsync();
            return this.NoContent();
        }
    }
}
