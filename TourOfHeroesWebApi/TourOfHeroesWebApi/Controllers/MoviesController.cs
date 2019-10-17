using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TourOfHeroesWebApi.Controllers
{
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using TourOfHeroesData.Models;
    using TourOfHeroesServices.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;

    [Authorize]
    public class MoviesController : ApiController
    {
        private readonly IMovieService _movieService;
        private readonly ILoggerManager _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public MoviesController(IMovieService movieService, ILoggerManager logger, UserManager<ApplicationUser> userManager)
        {
            _movieService = movieService;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpDelete("{title}")]
        [DisableRequestSizeLimit]
        [Route("movies/{title}")]
        public async Task<ActionResult<Hero>> DeleteMovie(string title, string password)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                _logger.LogInfo($"Deleting movie with id {title}...");

                await this._movieService.DeleteMovie(title);

                _logger.LogInfo($"Successfully deleted movie with id {title}...");

                return this.NoContent();
            }
            else
            {
                return Unauthorized(new { message = "Invalid password !" });
            }
        }
    }
}