namespace TourOfHeroesWebApi.Validator
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using TourOfHeroesData.Models;
    using Controllers;
    using Contracts;

    public class UserValidator : ApiController, IUserValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckPasswordAsync(string password)
        {
            try
            {
                var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

                var user = await _userManager.FindByIdAsync(userId);

                var isPasswordEqual = await _userManager.CheckPasswordAsync(user, password);

                return isPasswordEqual;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
