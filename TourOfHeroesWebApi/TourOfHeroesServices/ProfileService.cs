namespace TourOfHeroesServices
{
    using System;
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
        private readonly IRepository<UserBlogLikes> _userBlogLikesRepository;
        private readonly IRepository<UserBlogDislikes> _userBlogDislikesRepository;
        private readonly IRepository<UserActivity> _userActivityRepository;

        public ProfileService(IRepository<ApplicationUser> userRepository, IImageService imageService, IHubContext<ProfileImageHub, ITypedHubClient> hubContext, IRepository<UserBlogLikes> userBlogLikesRepository, IRepository<UserBlogDislikes> userBlogDislikesRepository, IRepository<UserActivity> userActivityRepository)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _hubContext = hubContext;
            _userBlogLikesRepository = userBlogLikesRepository;
            _userBlogDislikesRepository = userBlogDislikesRepository;
            _userActivityRepository = userActivityRepository;
        }

        public GetProfileDetailDTO GetProfile(string id)
        {
            var user = this._userRepository
                .All()
                .To<GetProfileDetailDTO>()
                .Single(x => x.Id == id);

            user.PostLikes = this._userBlogLikesRepository
                .All()
                .Count(x => x.UserId == id);

            user.PostDislikes = this._userBlogDislikesRepository
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

            var activity = new UserActivity
            {
                Action = $"Update profile image",
                RegisteredOn = DateTime.Now,
                UserId = id,
            };

            await this._hubContext.Clients.All.UpdateProfileImage(dbUser.ProfileImage);
            await this._userActivityRepository.AddAsync(activity);

            dbUser.Activity.Add(activity);

            await this._userRepository.SaveChangesAsync();
        }

        public async Task UpdateProfileEmail(string id, UpdateProfileEmailDTO emailDto)
        {
            var dbUser = this._userRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            dbUser.Email = emailDto.Email;

            var activity = new UserActivity
            {
                Action = $"Update profile email",
                RegisteredOn = DateTime.Now,
                UserId = id,
            };

            await this._userActivityRepository.AddAsync(activity);

            dbUser.Activity.Add(activity);

            await this._userRepository.SaveChangesAsync();
        }
    }
}