using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourOfHeroesData.Models;
using TourOfHeroesDTOs;
using TourOfHeroesServices.Contracts;

namespace TourOfHeroesWebApi.Controllers
{
    public class HeroesController : ApiController
    {
        private readonly IHeroService _heroService;

        public HeroesController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpGet, DisableRequestSizeLimit]
        public ActionResult<IEnumerable<Hero>> Get()
        {
            return this._heroService.GetAllHeroes().ToList();
        }

        [HttpGet("{id}"), DisableRequestSizeLimit]
        [Route("heroes/{id}")]
        public ActionResult<Hero> Get(int id)
        {
            var hero = this._heroService.GetById(id);
            return hero;
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("get-heroes")]
        public ActionResult<IEnumerable<Hero>> GetHeroesBySearchString(string name)
        {
            var heroes = this._heroService.GetHeroBySearchString(name).ToList();
            if (heroes.Count != 0)
                return heroes;
            return this.NoContent();
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<Hero>> CreateHero([FromForm] CreateHeroDTO hero)
        {
            if (!ModelState.IsValid) return this.NoContent();
            await this._heroService.CreateHero(hero);
            return this.CreatedAtAction("Get", new { name = hero.Name });

        }

        [HttpPut("{id}"), DisableRequestSizeLimit]
        [Route("heroes/{id}")]
        public async Task<IActionResult> UpdateHero(int id, Hero hero)
        {
            var dbHero = this._heroService.GetById(id);

            if (dbHero != null)
            {
                await this._heroService.UpdateHero(id, hero);
            }

            return this.NoContent();
        }

        [HttpDelete("{id}"), DisableRequestSizeLimit]
        [Route("heroes/{id}")]
        public async Task<ActionResult<Hero>> Delete(int id)
        {
            var hero = this._heroService.GetById(id);

            if (hero == null)
            {
                return this.NotFound();
            }

            await this._heroService.DeleteHero(id);
            return this.NoContent();
        }
    }
}
