namespace TourOfHeroesWebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TourOfHeroesServices.Contracts;
    using TourOfHeroesDTOs;

    [Authorize]
    public class ProfileController : ApiController
    {
        private readonly IProfileService _profileService;
        private readonly ILoggerManager _logger;

        public ProfileController(IProfileService profileService, ILoggerManager logger)
        {
            _profileService = profileService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [Route("profile/{id}")]
        public ActionResult<GetUserDetailDTO> UserDetail(string id)
        {
            _logger.LogInfo($"Fetching user with id {id}...");

            var user = this._profileService.GetUser(id);

            _logger.LogInfo($"User with id {id} successfully fetched.");

            return user;
        }
    }
}