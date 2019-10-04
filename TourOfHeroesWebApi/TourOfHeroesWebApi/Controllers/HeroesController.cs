using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        #region GetAllHeroes

        [HttpGet("all"), DisableRequestSizeLimit]
        [Route("heroes/{all}")]
        public PageResultDTO<GetHeroDTO> GetAllHeroes(int? page, int pageSize = 6)
        {
            var countDetails = _heroService.GetAllHeroes().Count();
            var result = new PageResultDTO<GetHeroDTO>
            {
                Count = countDetails,
                PageIndex = page ?? 1,
                PageSize = pageSize,
                Items = _heroService.GetAllHeroes().Skip((page - 1 ?? 0) * pageSize).Take(pageSize).ToList()
            };
            return result;
        }

        #endregion

        #region GetHeroById

        [HttpGet("{id}"), DisableRequestSizeLimit]
        [Route("heroes/{id}")]
        public ActionResult<GetHeroDetailDTO> GetHeroById(int id)
        {
            var hero = this._heroService.GetById(id);

            return hero;
        }

        #endregion

        #region GetHeroesBySearchString

        [HttpGet("get-heroes"), DisableRequestSizeLimit]
        [Route("heroes/{get-heroes}")]
        public ActionResult<IEnumerable<GetHeroDTO>> GetHeroesBySearchString(string name)
        {
            var heroes = this._heroService.GetHeroBySearchString(name).ToList();
            if (heroes.Count != 0)
                return heroes;
            return this.NoContent();
        }

        #endregion

        #region CreateHero

        [HttpPost("create-hero"), DisableRequestSizeLimit]
        [Route("heroes/{create-hero}")]
        public async Task<ActionResult<CreateHeroDTO>> CreateHero([FromForm] CreateHeroDTO hero)
        {
            if (!ModelState.IsValid) return this.NoContent();
            try
            {
                await this._heroService.CreateHero(hero);
            }
            //

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            return this.CreatedAtAction("GetAllHeroes", new { name = hero.Name });
        }

        #endregion

        #region UpdateHero

        [HttpPut("{id}"), DisableRequestSizeLimit]
        [Route("heroes/{id}")]
        public async Task<IActionResult> UpdateHero(int id, UpdateHeroDTO hero)
        {
            var dbHero = this._heroService.GetById(id);

            if (dbHero != null)
            {
                await this._heroService.UpdateHero(id, hero);
            }

            return this.NoContent();
        }

        #endregion

        #region DeleteHero

        [HttpDelete("{id}"), DisableRequestSizeLimit]
        [Route("heroes/{id}")]
        public async Task<ActionResult<Hero>> DeleteHero(int id)
        {
            var hero = this._heroService.GetById(id);

            if (hero == null)
            {
                return this.NotFound();
            }

            await this._heroService.DeleteHero(id);
            return this.NoContent();
        }

        #endregion
    }
}
