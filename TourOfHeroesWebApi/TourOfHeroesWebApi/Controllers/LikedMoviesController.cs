using System;

namespace TourOfHeroesWebApi.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using TourOfHeroesData.Models;
    using TourOfHeroesServices.Contracts;
    using TourOfHeroesDTOs.HeroDtos;
    using TourOfHeroesDTOs.MovieDtos;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class LikedMoviesController : ApiController
    {
        private readonly ILoggerManager _logger;
        private readonly IMovieService _movieService;

        public LikedMoviesController(ILoggerManager logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        #region GetLikedMovies

        [HttpGet("{likes}")]
        [DisableRequestSizeLimit]
        [Route("likedmovies/{likes}")]
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
        [Route("likedmovies/{like}")]
        public async Task<ActionResult<AddToLikesMovieDTO>> LikeMovie([FromForm] AddToLikesMovieDTO movie)
        {
            try
            {
                _logger.LogInfo($"Adding movie with title {movie.Title} to liked...");

                await this._movieService.LikeMovie(movie);

                _logger.LogInfo($"Successfully liked movie with title {movie.Title}...");

                return this.Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion

        #region DislikeMovie

        [HttpDelete("{dislike}")]
        [DisableRequestSizeLimit]
        [Route("likedmovies/{dislike}")]
        public async Task<ActionResult<LikedMovie>> DislikeMovie(string movieId)
        {
            _logger.LogInfo($"Disliking movie with id {movieId}...");

            await this._movieService.DislikeMovie(int.Parse(movieId));

            _logger.LogInfo($"Successfully disliked movie with id {movieId}...");

            return this.NoContent();
        }

        #endregion
    }
}