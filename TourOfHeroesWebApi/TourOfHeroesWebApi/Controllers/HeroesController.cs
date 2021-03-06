﻿namespace TourOfHeroesWebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.HeroDtos;
    using TourOfHeroesServices.Contracts;

    using Validator.Contracts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class HeroesController : ApiController
    {
        private readonly IHeroService _heroService;
        private readonly ILoggerManager _logger;
        private readonly IUserValidator _userValidator;

        public HeroesController(IHeroService heroService, ILoggerManager logger, IUserValidator userValidator)
        {
            _heroService = heroService;
            _logger = logger;
            _userValidator = userValidator;
        }

        #region GetAllHeroes

        [HttpGet("all")]
        [DisableRequestSizeLimit]
        [Route("heroes/{all}")]
        public PageResultDTO<GetHeroDTO> GetAllHeroes(int? page, int pageSize = 9)
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
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            _logger.LogInfo($"Fetching hero with id {id}...");

            var hero = this._heroService.GetById(userId, id);

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
        [Authorize(Roles = "Admin, Editor")]
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
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            var dbHero = this._heroService.GetById(userId, id);

            _logger.LogInfo($"Update hero with {id}...");

            if (dbHero == null) return NotFound(new { message = "Hero that you are trying to update is not found." });

            if (dbHero.Name == hero.Name)
            {
                return BadRequest(new { message = "Name that you are trying to enter is equal to previous !" });
            }

            await this._heroService.UpdateHero(id, hero);

            _logger.LogInfo($"Hero with {id} successfully updated.");

            return this.NoContent();
        }

        #endregion

        #region DeleteHero

        [HttpDelete("{id}")]
        [DisableRequestSizeLimit]
        [Route("heroes/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Hero>> DeleteHero(int id, string password)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            if (!this._userValidator.CheckPasswordAsync(userId, password).Result)
                return BadRequest(new {message = "Invalid password !"});

            var hero = this._heroService.GetById(userId, id);

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