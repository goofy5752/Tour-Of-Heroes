﻿namespace TourOfHeroesWebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TourOfHeroesDTOs.ProfileDtos;
    using TourOfHeroesServices.Contracts;
    using System.Threading.Tasks;

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
        [DisableRequestSizeLimit]
        [Route("profile/{id}")]
        public ActionResult<GetUserDetailDTO> GetDetail(string id)
        {
            _logger.LogInfo($"Fetching user with id {id}...");

            var user = this._profileService.GetUser(id);

            _logger.LogInfo($"User with id {id} successfully fetched.");

            return user;
        }

        [HttpPut("{id}")]
        [DisableRequestSizeLimit]
        [Route("profile/{id}")]
        public async Task<IActionResult> UpdateEmail(string id, UpdateProfileEmailDTO emailDto)
        {
            _logger.LogInfo($"Updating profile with username {id}...");

            if (emailDto.Email == "") return BadRequest();

            await this._profileService.UpdateProfileEmail(id, emailDto);

            _logger.LogInfo($"Profile with id: {id} successfully updated.");

            return NoContent();
        }

        [HttpPost("{id}")]
        [DisableRequestSizeLimit]
        [Route("profile/{id}")]
        public async Task<IActionResult> UpdateImage([FromForm] UpdateProfileImageDTO profileImageDto)
        {
            _logger.LogInfo($"Updating profile with username {profileImageDto.UserId}...");

            if (profileImageDto.ProfileImage == null) return BadRequest();

            await this._profileService.UpdateProfileImage(profileImageDto.UserId, profileImageDto);

            _logger.LogInfo($"Profile with id: {profileImageDto.UserId} successfully updated.");

            return NoContent();
        }
    }
}