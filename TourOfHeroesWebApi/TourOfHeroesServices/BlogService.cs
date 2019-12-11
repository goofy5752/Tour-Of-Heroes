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

        public BlogService(IRepository<Blog> blogRepository, IRepository<ApplicationUser> userRepository, IImageService imageService)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _imageService = imageService;
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

            var postLike = new UserBlog
            {
                Blog = blog,
                BlogId = blogId,
                User = user,
                UserId = userId
            };

            blog?.BlogUsers.Add(postLike);
            user?.UserBlogs.Add(postLike);

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