namespace TourOfHeroesServices
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.BlogDtos;
    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesMapping.Mapping;

    using Contracts;

    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> _blogRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IImageService _imageService;
        private readonly IRepository<UserBlogLikes> _userBlogLikesRepository;
        private readonly IRepository<UserBlogDislikes> _userBlogDislikesRepository;

        public BlogService(IRepository<Blog> blogRepository, IRepository<ApplicationUser> userRepository, IImageService imageService, IRepository<UserBlogLikes> userBlogLikesRepository, IRepository<UserBlogDislikes> userBlogDislikesRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _imageService = imageService;
            _userBlogLikesRepository = userBlogLikesRepository;
            _userBlogDislikesRepository = userBlogDislikesRepository;
        }

        public IEnumerable<GetPostDTO> GetAllPosts()
        {
            var allPosts = this._blogRepository
                .All()
                .To<GetPostDTO>()
                .ToList();

            return allPosts;
        }

        public GetPostDetailDTO GetPostDetail(int id)
        {
            var post = this._blogRepository
                .All()
                .To<GetPostDetailDTO>()
                .SingleOrDefault(x => x.Id == id);

            if (post != null)
            {
                // ReSharper disable once PossibleNullReferenceException
                post.Likes = this._userBlogLikesRepository
                    .All()
                    .Count(x => x.BlogId == id);

                post.Dislikes = this._userBlogDislikesRepository
                    .All()
                    .Count(x => x.BlogId == id);
            }

            return post;
        }

        public async Task LikePost(string userId, int blogId)
        {
            var blog = this._blogRepository
                .All()
                .FirstOrDefault(x => x.Id == blogId);

            var user = this._userRepository
                .All()
                .FirstOrDefault(x => x.Id == userId);

            if (this._userBlogDislikesRepository.All().Any(x => x.UserId == userId && x.BlogId == blogId))
            {
                var entityToDelete = this._userBlogDislikesRepository
                    .All()
                    .FirstOrDefault(x => x.BlogId == blogId && x.UserId == userId);

                this._userBlogDislikesRepository.Delete(entityToDelete);
            }

            var postLike = new UserBlogLikes
            {
                Blog = blog,
                BlogId = blogId,
                User = user,
                UserId = userId
            };

            blog?.BlogUserLikes.Add(postLike);
            user?.UserBlogLikes.Add(postLike);

            await this._userRepository.SaveChangesAsync();
        }

        public async Task DislikePost(string userId, int blogId)
        {
            var blog = this._blogRepository
                .All()
                .FirstOrDefault(x => x.Id == blogId);

            var user = this._userRepository
                .All()
                .FirstOrDefault(x => x.Id == userId);

            if (this._userBlogLikesRepository.All().Any(x => x.UserId == userId && x.BlogId == blogId))
            {
                var entityToDelete = this._userBlogLikesRepository
                    .All()
                    .FirstOrDefault(x => x.BlogId == blogId && x.UserId == userId);

                this._userBlogLikesRepository.Delete(entityToDelete);
            }

            var postDislike = new UserBlogDislikes
            {
                Blog = blog,
                BlogId = blogId,
                User = user,
                UserId = userId
            };

            blog?.BlogUserDislikes.Add(postDislike);
            user?.UserBlogDislikes.Add(postDislike);

            await this._userRepository.SaveChangesAsync();
        }

        public async Task CreatePost(CreateBlogPostDTO postDto, string userId)
        {
            var userObj = this._userRepository.All().FirstOrDefault(x => x.Id == userId);

            if (userObj == null) return;

            var blogImageUrl = this._imageService.AddToCloudinaryAndReturnBlogImageUrl(postDto.BlogImage);

            var postObj = new Blog
            {
                Content = postDto.Content,
                Title = postDto.Title,
                AuthorUserName = userObj.UserName,
                BlogImage = $"{blogImageUrl}",
                PublishedOn = DateTime.Now,
                UserId = userObj.Id
            };

            userObj.Blogs.Add(postObj);

            await this._blogRepository.SaveChangesAsync();
        }

        public async Task DeletePost(int id)
        {
            var postToDelete = this._blogRepository.All().FirstOrDefault(x => x.Id == id);

            if (postToDelete != null) this._blogRepository.Delete(postToDelete);

            await this._blogRepository.SaveChangesAsync();
        }
    }
}