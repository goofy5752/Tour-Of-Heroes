namespace TourOfHeroesWebApi.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using TourOfHeroesDTOs;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using TourOfHeroesCommon;
    using TourOfHeroesData.Common;
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
                FullName = model.FullName
            };

            try
            {
                _logger.LogInfo($"Creating account with username {model.UserName}...");

                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, GlobalConstants.UserRole);

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
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("UserID",user.Id)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                _logger.LogInfo($"Account with username {model.UserName} successfully logged !");
                return Ok(new { token });
            }

            _logger.LogError($"Username or password is incorrect.");
            return BadRequest(new {message = "Username or password is incorrect."});
        }
    }
}