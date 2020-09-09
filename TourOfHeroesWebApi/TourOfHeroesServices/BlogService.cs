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
        private readonly IRepository<UserActivity> _userActivityRepository;
        private readonly IRepository<Comment> _commentRepository;

        public BlogService(IRepository<Blog> blogRepository, IRepository<ApplicationUser> userRepository, IImageService imageService, IRepository<UserBlogLikes> userBlogLikesRepository, IRepository<UserBlogDislikes> userBlogDislikesRepository, IRepository<UserActivity> userActivityRepository, IRepository<Comment> commentRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _imageService = imageService;
            _userBlogLikesRepository = userBlogLikesRepository;
            _userBlogDislikesRepository = userBlogDislikesRepository;
            _userActivityRepository = userActivityRepository;
            _commentRepository = commentRepository;
        }

        public IEnumerable<GetPostDTO> GetAllPosts()
        {
            var allPosts = this._blogRepository
                .All()
                .To<GetPostDTO>()
                .ToList();

            return allPosts;
        }

        public GetPostDetailDTO GetPostDetail(string currentUser, int id)
        {
            var post = this._blogRepository
                .All()
                .To<GetPostDetailDTO>()
                .Single(x => x.Id == id);

            post.CurrentUser = this._userRepository
                .All()
                .Single(x => x.Id == currentUser)
                .UserName;

            post.IsLiked = false;
            post.IsDisliked = false;

            if (this._userBlogLikesRepository.All().FirstOrDefault(x => x.UserId == currentUser && x.BlogId == id) != null)
            {
                post.IsLiked = true;
            }
            else if (this._userBlogDislikesRepository.All().FirstOrDefault(x => x.UserId == currentUser && x.BlogId == id) != null)
            {
                post.IsDisliked = true;
            }

            post.Likes = this._userBlogLikesRepository
                .All()
                .Count(x => x.BlogId == id);

            post.Dislikes = this._userBlogDislikesRepository
                .All()
                .Count(x => x.BlogId == id);

            post.LatestPosts = this._blogRepository
                .All()
                .OrderBy(x => x.PublishedOn)
                .Take(6)
                .To<GetLatestPostsDTO>()
                .ToList();

            return post;
        }

        public async Task LikePost(string userId, int blogId)
        {
            var blog = this._blogRepository
                .All()
                .Single(x => x.Id == blogId);

            var user = this._userRepository
                .All()
                .Single(x => x.Id == userId);

            //Check if user have already like the post
            if (this._userBlogLikesRepository.All().Any(x => x.UserId == userId && x.BlogId == blogId))
            {
                throw new Exception();
            }

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

            var activity = new UserActivity
            {
                Action = $"Like blog with title {blog?.Title}.",
                RegisteredOn = DateTime.Now,
                User = user,
                UserId = user?.Id
            };

            blog?.BlogUserLikes.Add(postLike);
            user?.UserBlogLikes.Add(postLike);
            await this._userActivityRepository.AddAsync(activity);
            user?.Activity.Add(activity);

            await this._userRepository.SaveChangesAsync();
        }

        public async Task DislikePost(string userId, int blogId)
        {
            var blog = this._blogRepository
                .All()
                .Single(x => x.Id == blogId);

            var user = this._userRepository
                .All()
                .Single(x => x.Id == userId);

            //Check if user have already dislike the post
            if (this._userBlogDislikesRepository.All().Any(x => x.UserId == userId && x.BlogId == blogId))
            {
                throw new Exception();
            }

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

            var activity = new UserActivity
            {
                Action = $"Dislike blog with title {blog?.Title}.",
                RegisteredOn = DateTime.Now,
                User = user,
                UserId = user?.Id
            };

            blog?.BlogUserDislikes.Add(postDislike);
            user?.UserBlogDislikes.Add(postDislike);
            await this._userActivityRepository.AddAsync(activity);
            user?.Activity.Add(activity);

            await this._userRepository.SaveChangesAsync();
        }

        public async Task CreatePost(CreateBlogPostDTO postDto, string userId, bool skipMethodForTest = false)
        {
            var userObj = this._userRepository.All().Single(x => x.Id == userId);

            string blogImageUrl = "";

            if (string.IsNullOrEmpty(postDto.Content) || string.IsNullOrEmpty(postDto.Title) ||
                postDto.BlogImage == null)
            {
                throw new InvalidOperationException("Fields cannot be null or empty.");
            }

            if (!skipMethodForTest)
            {
                blogImageUrl = this._imageService.AddToCloudinaryAndReturnBlogImageUrl(postDto.BlogImage);
            }

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

        public async Task EditPost(EditBlogPostDTO postDto, bool skipMethodForTest = false)
        {
            try
            {
                var postObj = this._blogRepository.All().Single(x => x.Id == int.Parse(postDto.Id));

                postObj.Content = postDto.Content;
                postObj.Title = postDto.Title;

                await this._blogRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Invalid post id.", e.InnerException);
            }
        }

        public async Task DeletePost(int id)
        {
            var postToDelete = this._blogRepository.All().Single(x => x.Id == id);

            var commentsToDelete = this._commentRepository
                .All()
                .Where(c => c.BlogId == id)
                .ToList();

            foreach (var comment in commentsToDelete)
            {
                this._commentRepository.Delete(comment);
            }

            this._blogRepository.Delete(postToDelete);

            await this._blogRepository.SaveChangesAsync();
        }
    }
}