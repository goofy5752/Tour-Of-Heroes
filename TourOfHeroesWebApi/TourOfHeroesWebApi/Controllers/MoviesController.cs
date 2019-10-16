namespace TourOfHeroesWebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TourOfHeroesData.Models;
    using TourOfHeroesServices.Contracts;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class MoviesController : ApiController
    {
        private readonly IMovieService _movieService;
        private readonly ILoggerManager _logger;

        public MoviesController(IMovieService movieService, ILoggerManager logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpDelete("{title}")]
        [DisableRequestSizeLimit]
        [Route("movies/{title}")]
        public async Task<ActionResult<Hero>> DeleteMovie(string title)
        {
            var movie = this._movieService.GetAllMovies();

            if (movie == null)
            {
                return this.NotFound();
            }

            _logger.LogInfo($"Deleting movie with id {title}...");

            await this._movieService.DeleteMovie(title);

            _logger.LogInfo($"Successfully deleted movie with id {title}...");

            return this.NoContent();
        }
    }
}