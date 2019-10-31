namespace TourOfHeroesServices
{
    using System.Threading.Tasks;
    using Contracts;
    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using Microsoft.AspNetCore.SignalR;
    using RealTimeHub;
    using TourOfHeroesDTOs.ProfileDtos;
    using System.Linq;
    using TourOfHeroesMapping.Mapping;

    public class ProfileService : IProfileService
    {
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IImageService _imageService;
        private readonly IHubContext<ProfileImageHub, ITypedHubClient> _hubContext;

        public ProfileService(IRepository<ApplicationUser> userRepository, IImageService imageService, IHubContext<ProfileImageHub, ITypedHubClient> hubContext)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _hubContext = hubContext;
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

            await this._hubContext.Clients.All.UpdateProfileImage(dbUser.ProfileImage);

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