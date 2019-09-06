using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourOfHeroesWebApi.Data;
using TourOfHeroesWebApi.Data.Models;

namespace TourOfHeroesWebApi.Controllers
{
    public class HeroesController : ApiController
    {
        private readonly TourOfHeroesDbContext _dbContext;

        public HeroesController(TourOfHeroesDbContext dbContext)
        {
            _dbContext = dbContext;
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

        [HttpPost]
        public async Task<ActionResult<Hero>> Post(Hero hero)
        {
            await this._dbContext.Heroes.AddAsync(hero);
            await this._dbContext.SaveChangesAsync();
            return this.CreatedAtAction("Get", new { id = hero.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Hero hero, int id)
        {
            var dbHero = this._dbContext.Heroes.FirstOrDefault(x => x.Id == id);
            dbHero.Name = hero.Name;
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
            return hero;
        }
    }
}
