using Microsoft.AspNetCore.Mvc;

namespace TourOfHeroesServices
{
    using System.Threading.Tasks;
    using TourOfHeroesDTOs;
    using Contracts;
    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using System.Linq;
    using TourOfHeroesMapping.Mapping;


    public class ProfileService : IProfileService
    {
        private readonly IRepository<ApplicationUser> _userRepository;

        public ProfileService(IRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public GetUserDetailDTO GetUser(string id)
        {
            var user = this._userRepository
                .All()
                .To<GetUserDetailDTO>()
                .Single(x => x.Id == id);

            return user;
        }

        public async Task UpdateProfileImage(string id, UpdateProfileImageDTO profile)
        {
            var dbUser = this._userRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            await this._userRepository.SaveChangesAsync();
        }

        public async Task UpdateProfileEmail(string id, UpdateProfileEmailDTO emailDto)
        {
            var dbUser = this._userRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            dbUser.Email = emailDto.Email;

            await this._userRepository.SaveChangesAsync();
        }
    }
}