namespace TourOfHeroesWebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using TourOfHeroesServices.Contracts;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.BlogDtos;
    using System.Linq;
    using TourOfHeroesDTOs.HeroDtos;

    [Authorize]
    public class BlogController : ApiController
    {
        private readonly IBlogService _blogService;
        private readonly ILoggerManager _logger;

        public BlogController(IBlogService blogService, ILoggerManager logger)
        {
            _blogService = blogService;
            _logger = logger;
        }

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

        [HttpPost("{create-post}")]
        [DisableRequestSizeLimit]
        [Route("blog/{create-post}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CreateBlogPostDTO>> CreatePost([FromForm]CreateBlogPostDTO postDto)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            if (!ModelState.IsValid) return this.NoContent();

            _logger.LogInfo("Creating a new post...");

            await this._blogService.CreatePost(postDto, userId);

            _logger.LogInfo($"Post with content {postDto.Content} successfully created.");

            return this.CreatedAtAction("CreatePost", new { text = postDto.Content });
        }

        [HttpDelete("{id}")]
        [DisableRequestSizeLimit]
        [Route("blog/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Hero>> DeletePost(int id)
        {
            var post = this._blogService.GetAllPosts();

            if (post == null)
            {
                return this.NotFound();
            }

            _logger.LogInfo($"Deleting post with id {id}...");

            await this._blogService.DeletePost(id);

            _logger.LogInfo($"Successfully deleted post with id {id}...");

            return this.NoContent();
        }
    }
}