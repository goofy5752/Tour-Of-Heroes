namespace TourOfHeroesWebApi.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using TourOfHeroesServices.Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.BlogDtos;
    using TourOfHeroesDTOs.HeroDtos;

    using Validator.Contracts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class BlogController : ApiController
    {
        private readonly IBlogService _blogService;
        private readonly ILoggerManager _logger;
        private readonly IUserValidator _userValidator;

        public BlogController(IBlogService blogService, ILoggerManager logger, IUserValidator userValidator)
        {
            _blogService = blogService;
            _logger = logger;
            _userValidator = userValidator;
        }

        #region GetAllPosts

        [HttpGet("all")]
        [DisableRequestSizeLimit]
        [Route("blog/{all}")]
        public PageResultDTO<GetPostDTO> GetAllPosts(int? page, int pageSize = 6)
        {
            _logger.LogInfo("Fetching all the heroes from the storage...");

            var countDetails = this._blogService.GetAllPosts().Count();
            var result = new PageResultDTO<GetPostDTO>
            {
                Count = countDetails,
                PageIndex = page ?? 1,
                PageSize = pageSize,
                Items = this._blogService.GetAllPosts().Skip((page - 1 ?? 0) * pageSize).Take(pageSize).ToList()
            };

            _logger.LogInfo($"Returning {countDetails} heroes.");

            return result;
        }

        #endregion

        #region PostDetail

        [HttpGet("{id}")]
        [DisableRequestSizeLimit]
        [Route("blog/{id}")]
        public ActionResult<GetPostDetailDTO> PostDetail(int id)
        {
            _logger.LogInfo($"Fetching hero with id {id}...");

            var detail = this._blogService.GetPostDetail(id);

            _logger.LogInfo($"Hero with id {id} successfully fetched.");

            return detail;
        }

        #endregion

        #region CreatePost

        [HttpPost("{create-post}")]
        [DisableRequestSizeLimit]
        [Route("blog/{create-post}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<ActionResult<CreateBlogPostDTO>> CreatePost([FromForm]CreateBlogPostDTO postDto)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            if (!ModelState.IsValid) return this.NoContent();

            _logger.LogInfo("Creating a new post...");

            await this._blogService.CreatePost(postDto, userId);

            _logger.LogInfo($"Post with content {postDto.Content} successfully created.");

            return this.CreatedAtAction("CreatePost", new { text = postDto.Content });
        }

        #endregion

        #region LikePost

        [DisableRequestSizeLimit]
        [Route("like")]
        public async Task LikePost(LikePostDTO postDto)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            _logger.LogInfo($"Liked post with id {postDto.PostId} ...");

            await this._blogService.LikePost(userId, int.Parse(postDto.PostId));

            _logger.LogInfo($"Successfully liked post with id {postDto.PostId}...");
        }
        #endregion

        #region DeletePost

        [HttpDelete("{id}")]
        [DisableRequestSizeLimit]
        [Route("blog/{id}")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<ActionResult<Blog>> DeletePost(int id, string password)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            if (!this._userValidator.CheckPasswordAsync(userId, password).Result)
                return BadRequest(new { message = "Invalid password !" });

            var postDetail = this._blogService.GetPostDetail(id);

            if (postDetail == null)
            {
                return this.NotFound();
            }

            _logger.LogInfo($"Deleting post with id {id}...");

            await this._blogService.DeletePost(id);

            _logger.LogInfo($"Successfully deleted post with id {id}...");

            return this.NoContent();
        }

        #endregion
    }
}