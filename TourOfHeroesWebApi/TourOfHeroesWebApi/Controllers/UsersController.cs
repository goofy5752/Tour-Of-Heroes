namespace TourOfHeroesWebApi.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.HeroDtos;
    using TourOfHeroesServices.Contracts;
    using TourOfHeroesDTOs.UserDtos;
    using Validator.Contracts;

    [Authorize(Roles = "Admin")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        private readonly ILoggerManager _logger;
        private readonly IUserValidator _userValidator;

        public UsersController(IUserService userService, ILoggerManager logger, IUserValidator userValidator)
        {
            _userService = userService;
            _logger = logger;
            _userValidator = userValidator;
        }

        #region GetAllUsers

        [HttpGet("all")]
        [DisableRequestSizeLimit]
        [Route("users/{all}")]
        public PageResultDTO<GetUserDTO> GetAllUsers(int? page, int pageSize = 20)
        {
            _logger.LogInfo("Fetching all the users from the storage...");

            var countDetails = this._userService.GetAllUsers().Count();
            var result = new PageResultDTO<GetUserDTO>
            {
                Count = countDetails,
                PageIndex = page ?? 1,
                PageSize = pageSize,
                Items = this._userService.GetAllUsers().Skip((page - 1 ?? 0) * pageSize).Take(pageSize).ToList()
            };

            _logger.LogInfo($"Returning {countDetails} users.");

            return result;
        }

        #endregion

        #region GetUserDetail

        [HttpGet("{id}")]
        [DisableRequestSizeLimit]
        [Route("users/{id}")]
        public ActionResult<GetUserDetailDTO> GetUserDetail(string id)
        {
            _logger.LogInfo($"Fetching user with id {id}...");

            var hero = this._userService.GetUserDetail(id);

            _logger.LogInfo($"User with id {id} successfully fetched.");

            return hero;
        }

        #endregion

        #region UpdateUser

        [HttpPut("{id}")]
        [DisableRequestSizeLimit]
        [Route("users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserDTO userDTO)
        {
            var dbUser = this._userService.GetUserDetail(id);

            _logger.LogInfo($"Update user with {id}...");

            if (dbUser == null) return NotFound(new { message = "User that you are trying to update is not found." });

            //TODO: Find a way to implement logic for user role change ...

            //if (dbUser.Name == hero.Name)
            //{
            //    return BadRequest(new { message = "Name that you are trying to enter is equal to previous !" });
            //}

            await this._userService.UpdateUser(id, userDTO);

            _logger.LogInfo($"Hero with {id} successfully updated.");

            return this.NoContent();
        }

        #endregion

        #region DeleteUser

        [HttpDelete("{id}")]
        [DisableRequestSizeLimit]
        [Route("users/{id}")]
        public async Task<ActionResult<ApplicationUser>> DeleteUser(string id, string password)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            if (!this._userValidator.CheckPasswordAsync(userId, password).Result)
                return BadRequest(new { message = "Invalid password !" });

            var user = this._userService.GetUserDetail(id);

            _logger.LogInfo($"Deleting user with id {id}...");

            if (user == null)
            {
                return this.NotFound();
            }

            await this._userService.DeleteUser(id);

            _logger.LogInfo($"User with id {id} successfully deleted.");

            return this.NoContent();
        }

        #endregion
    }
}