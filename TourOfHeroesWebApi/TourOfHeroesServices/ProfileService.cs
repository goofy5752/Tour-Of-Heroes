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
        private readonly IImageService _imageService;

        public ProfileService(IRepository<ApplicationUser> userRepository, IImageService imageService)
        {
            _userRepository = userRepository;
            _imageService = imageService;
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

            var profileImage = this._imageService.AddToCloudinaryAndReturnProfileImageUrl(profile.ProfileImage);

            dbUser.ProfileImage = profileImage;

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