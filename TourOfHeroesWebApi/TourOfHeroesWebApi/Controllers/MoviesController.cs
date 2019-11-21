namespace TourOfHeroesWebApi.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using TourOfHeroesData.Models;
    using TourOfHeroesServices.Contracts;
    using Validator.Contracts;
    using TourOfHeroesDTOs.MovieDtos;
    using TourOfHeroesDTOs.HeroDtos;

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

        #region GetLikedMovies

        [HttpGet("{likes}")]
        [DisableRequestSizeLimit]
        [Route("movies/{likes}")]
        public PageResultDTO<GetLikedMovieDTO> GetLikedMovies(int? page, int pageSize = 12)
        {
            _logger.LogInfo("Fetching all the liked movies from the storage...");

            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            var countDetails = this._movieService.GetLikedMovies(userId).Count();
            var result = new PageResultDTO<GetLikedMovieDTO>
            {
                Count = countDetails,
                PageIndex = page ?? 1,
                PageSize = pageSize,
                Items = this._movieService.GetLikedMovies(userId).Skip((page - 1 ?? 0) * pageSize).Take(pageSize).ToList()
            };

            _logger.LogInfo($"Returning {countDetails} movies.");

            return result;
        }

        #endregion

        #region LikeMovie

        [HttpPost("{like}")]
        [DisableRequestSizeLimit]
        [Route("movies/{like}")]
        public async Task<ActionResult<AddToLikesMovieDTO>> LikeMovie([FromForm] AddToLikesMovieDTO movie)
        {
            _logger.LogInfo($"Adding movie with title {movie.Title} to liked...");

            await this._movieService.LikeMovie(movie);

            _logger.LogInfo($"Successfully liked movie with title {movie.Title}...");

            return this.Ok();
        }

        #endregion

        #region DeleteMovie

        [HttpDelete("{title}")]
        [DisableRequestSizeLimit]
        [Route("movies/{title}")]
        public async Task<ActionResult<Hero>> DeleteMovie(string title, string password)
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

        //TODO: IMPLEMENT THE RIGHT LOGIC FLOW FOR DISLIKE MOVIE BUTTON
        #region DislikeMovie

        [HttpDelete("{title}")]
        [DisableRequestSizeLimit]
        [Route("movies/{title}")]
        public async Task<ActionResult<Hero>> DislikeMovie(int movieId)
        {
            _logger.LogInfo($"Deleting movie with id {movieId}...");

            await this._movieService.DislikeMovie(movieId);

            _logger.LogInfo($"Successfully deleted movie with id {movieId}...");

            return this.NoContent();
        }

        #endregion
    }
}