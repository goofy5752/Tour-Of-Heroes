namespace TourOfHeroesWebApi.Controllers.Validator
{
    using System;
    using System.Threading.Tasks;

    using TourOfHeroesData.Models;

    using Contracts;

    using Microsoft.AspNetCore.Identity;

    public class UserValidator : ApiController, IUserValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public UserValidator() { }

        public async Task<bool> CheckPasswordAsync(string userId, string password)
        {
            try
            {
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