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

        [HttpGet("all"), DisableRequestSizeLimit]
        [Route("heroes/{all}")]
        public PageResultDTO<Hero> Get(int? page, int pageSize = 6)
        {
            var countDetails = _heroService.GetAllHeroes().Count();
            var result = new PageResultDTO<Hero>
            {
                Count = countDetails,
                PageIndex = page ?? 1,
                PageSize = pageSize,
                Items = _heroService.GetAllHeroes().Skip((page - 1 ?? 0) * pageSize).Take(pageSize).ToList()
            };
            return result;
        }

        [HttpGet("{id}"), DisableRequestSizeLimit]
        [Route("heroes/{id}")]
        public ActionResult<Hero> Get(int id)
        {
            var hero = this._heroService.GetById(id);
            return hero;
        }

        [HttpGet("get-heroes"), DisableRequestSizeLimit]
        [Route("heroes/{get-heroes}")]
        public ActionResult<IEnumerable<Hero>> GetHeroesBySearchString(string name)
        {
            var heroes = this._heroService.GetHeroBySearchString(name).ToList();
            if (heroes.Count != 0)
                return heroes;
            return this.NoContent();
        }

        [HttpPost("create-hero"), DisableRequestSizeLimit]
        [Route("heroes/{create-hero}")]
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
