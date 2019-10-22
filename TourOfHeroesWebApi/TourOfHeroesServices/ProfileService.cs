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

        public async Task UpdateUser(string id, UpdateUserDTO user)
        {
            var dbUser = this._userRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            dbUser.ProfileImage = user.ProfileImage;

            await this._userRepository.SaveChangesAsync();
        }
    }
}