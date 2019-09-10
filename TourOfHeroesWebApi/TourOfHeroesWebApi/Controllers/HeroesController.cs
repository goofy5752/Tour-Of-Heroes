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

        [HttpPost]
        public async Task<ActionResult<Hero>> Post(Hero hero)
        {
            await this._dbContext.Heroes.AddAsync(hero);
            await this._dbContext.SaveChangesAsync();
            return this.CreatedAtAction("Get", new { id = hero.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Hero hero)
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
            return this.NoContent();
        }
    }
}
