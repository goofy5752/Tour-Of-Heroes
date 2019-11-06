namespace TourOfHeroesWebApi.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using TourOfHeroesDTOs.UserDtos;
    using TourOfHeroesCommon;
    using TourOfHeroesData.Common;
    using System.Linq;
    using TourOfHeroesData.Models;
    using TourOfHeroesServices.Contracts;

    public class ApplicationUserController : ApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILoggerManager _logger;
        private readonly ApplicationSettings _appSettings;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, IOptions<ApplicationSettings> appSettings, ILoggerManager logger)
        {
            _userManager = userManager;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public async Task<object> Register(RegisterUserDTO model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg"
            };

            try
            {
                _logger.LogInfo($"Creating account with username {model.UserName}...");

                var result = await _userManager.CreateAsync(applicationUser, model.Password);

                if (model.EditorRoleCode == GlobalConstants.EditorRoleCode)
                {
                    await _userManager.AddToRoleAsync(applicationUser, GlobalConstants.EditorRole);
                }
                else
                {
                    await _userManager.AddToRoleAsync(applicationUser, GlobalConstants.UserRole);
                }

                _logger.LogInfo($"Account with username {model.UserName} successfully created !");

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginUserDTO model)
        {
            _logger.LogInfo($"Logging account with username {model.UserName}...");
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                _logger.LogError($"Username or password is incorrect.");
                return BadRequest(new { message = "Username or password is incorrect." });
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (user.LockoutEnd >= DateTime.UtcNow)
                {
                    _logger.LogError($"User have entered 3 invalid attempts");
                    user.AccessFailedCount = 0;
                    return Forbid();
                }

                user.AccessFailedCount = 0;

                //Get role assigned to the user
                var role = await _userManager.GetRolesAsync(user);
                var options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("UserID",user.Id),
                        new Claim(options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }

            await _userManager.AccessFailedAsync(user); // Register failed access

            return BadRequest(new { message = "Incorrect password." });
        }
    }
}