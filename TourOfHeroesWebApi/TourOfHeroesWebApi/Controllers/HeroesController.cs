namespace TourOfHeroesWebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs;
    using TourOfHeroesServices.Contracts;

    [Authorize]
    public class HeroesController : ApiController
    {
        private readonly IHeroService _heroService;
        private readonly ILoggerManager _logger;

        public HeroesController(IHeroService heroService, ILoggerManager logger)
        {
            _heroService = heroService;
            _logger = logger;
        }

        #region GetAllHeroes

        [HttpGet("all")]
        [DisableRequestSizeLimit]
        [Route("heroes/{all}")]
        public PageResultDTO<GetHeroDTO> GetAllHeroes(int? page, int pageSize = 6)
        {
            _logger.LogInfo("Fetching all the heroes from the storage...");

            var countDetails = _heroService.GetAllHeroes().Count();
            var result = new PageResultDTO<GetHeroDTO>
            {
                Count = countDetails,
                PageIndex = page ?? 1,
                PageSize = pageSize,
                Items = _heroService.GetAllHeroes().Skip((page - 1 ?? 0) * pageSize).Take(pageSize).ToList()
            };

            _logger.LogInfo($"Returning {countDetails} heroes.");

            return result;
        }

        #endregion

        #region GetHeroById
        
        [HttpGet("{id}")]
        [DisableRequestSizeLimit]
        [Route("heroes/{id}")]
        public ActionResult<GetHeroDetailDTO> GetHeroById(int id)
        {
            _logger.LogInfo($"Fetching hero with id {id}...");

            var hero = this._heroService.GetById(id);

            _logger.LogInfo($"Hero with id {id} successfully fetched.");

            return hero;
        }

        #endregion

        #region GetHeroesBySearchString
        
        [HttpGet("get-heroes")]
        [DisableRequestSizeLimit]
        [Route("heroes/{get-heroes}")]
        public ActionResult<IEnumerable<GetHeroDTO>> GetHeroesBySearchString(string name)
        {
            var heroes = this._heroService.GetHeroBySearchString(name).ToList();

            _logger.LogInfo($"Fetching heroes with search string {name}...");

            if (heroes.Count != 0)
                return heroes;

            _logger.LogInfo($"Heroes with search string {name} successfully fetched.");

            return this.NoContent();
        }

        #endregion

        #region CreateHero
        
        [HttpPost("create-hero")]
        [DisableRequestSizeLimit]
        [Route("heroes/{create-hero}")]
        public async Task<ActionResult<CreateHeroDTO>> CreateHero([FromForm] CreateHeroDTO hero)
        {
            if (!ModelState.IsValid) return this.NoContent();

            _logger.LogInfo("Creating a new hero...");

            await this._heroService.CreateHero(hero);

            _logger.LogInfo($"Hero with name {hero.Name} successfully created.");

            return this.CreatedAtAction("GetAllHeroes", new { name = hero.Name });
        }

        #endregion

        #region UpdateHero
        
        [HttpPut("{id}")]
        [DisableRequestSizeLimit]
        [Route("heroes/{id}")]
        public async Task<IActionResult> UpdateHero(int id, UpdateHeroDTO hero)
        {
            var dbHero = this._heroService.GetById(id);

            _logger.LogInfo($"Update hero with {id}...");

            if (dbHero != null)
            {
                await this._heroService.UpdateHero(id, hero);
            }

            _logger.LogInfo($"Hero with {id} successfully updated.");

            return this.NoContent();
        }

        #endregion

        #region DeleteHero

        [HttpDelete("{id}")]
        [DisableRequestSizeLimit]
        [Route("heroes/{id}")]
        public async Task<ActionResult<Hero>> DeleteHero(int id)
        {
            var hero = this._heroService.GetById(id);

            _logger.LogInfo($"Deleting hero with id {id}...");

            if (hero == null)
            {
                return this.NotFound();
            }

            await this._heroService.DeleteHero(id);

            _logger.LogInfo($"Hero with id {id} successfully deleted.");

            return this.NoContent();
        }

        #endregion
    }
}