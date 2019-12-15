namespace TourOfHeroesServices
{
    using System.Linq;
    using System.Threading.Tasks;

    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.ProfileDtos;
    using TourOfHeroesMapping.Mapping;

    using Contracts;
    using RealTimeHub;

    using Microsoft.AspNetCore.SignalR;

    public class ProfileService : IProfileService
    {
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IImageService _imageService;
        private readonly IHubContext<ProfileImageHub, ITypedHubClient> _hubContext;
        private readonly IRepository<UserBlog> _userBlogRepository;

        public ProfileService(IRepository<ApplicationUser> userRepository, IImageService imageService, IHubContext<ProfileImageHub, ITypedHubClient> hubContext, IRepository<UserBlog> userBlogRepository)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _hubContext = hubContext;
            _userBlogRepository = userBlogRepository;
        }

        public GetProfileDetailDTO GetProfile(string id)
        {
            var user = this._userRepository
                .All()
                .To<GetProfileDetailDTO>()
                .Single(x => x.Id == id);

            user.PostLikes = this._userBlogRepository
                .All()
                .Count(x => x.UserId == id);

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