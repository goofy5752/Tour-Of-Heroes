namespace TourOfHeroesWebApi.Controllers
{
    using System.Linq;
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

        #region DeleteMovie

        [HttpDelete("{title}")]
        [DisableRequestSizeLimit]
        [Route("movies/{title}")]
        public async Task<ActionResult<Movie>> DeleteMovie(string title, string password)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            if (!this._userValidator.CheckPasswordAsync(userId, password).Result)
                return BadRequest(new { message = "Invalid password !" });

            _logger.LogInfo($"Deleting movie with id {title}...");

            await this._movieService.DeleteMovie(title);

            _logger.LogInfo($"Successfully deleted movie with id {title}...");

            return this.NoContent();
        }

        #endregion
    }
}