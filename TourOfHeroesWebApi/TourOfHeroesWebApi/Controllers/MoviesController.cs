namespace TourOfHeroesWebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using TourOfHeroesData.Models;
    using TourOfHeroesServices.Contracts;
    using Validator.Contracts;

    [Authorize]
    public class MoviesController : ApiController
    {
        private readonly IMovieService _movieService;
        private readonly ILoggerManager _logger;
        private readonly IUserValidator _userValidator;

        public MoviesController(IMovieService movieService, ILoggerManager logger, IUserValidator userValidator)
        {
            _movieService = movieService;
            _logger = logger;
            _userValidator = userValidator;
        }

        [HttpDelete("{title}")]
        [DisableRequestSizeLimit]
        [Route("movies/{title}")]
        public async Task<ActionResult<Hero>> DeleteMovie(string title, string password)
        {
            if (!this._userValidator.CheckPasswordAsync(password).Result)
                return BadRequest(new {message = "Invalid password !"});

            _logger.LogInfo($"Deleting movie with id {title}...");

            await this._movieService.DeleteMovie(title);

            _logger.LogInfo($"Successfully deleted movie with id {title}...");

            return this.NoContent();
        }
    }
}