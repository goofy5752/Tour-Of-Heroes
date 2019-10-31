namespace TourOfHeroesWebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using TourOfHeroesServices.Contracts;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.BlogDtos;

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

        [HttpPost("{create-post}")]
        [DisableRequestSizeLimit]
        [Route("blog/create-post")]
        public async Task<ActionResult<CreateBlogPostDTO>> CreatePost(CreateBlogPostDTO postDto)
        {
            if (!ModelState.IsValid) return this.NoContent();

            _logger.LogInfo("Creating a new post...");

            await this._blogService.CreatePost(postDto);

            _logger.LogInfo($"Post with content {postDto.Content} successfully created.");

            return this.CreatedAtAction("CreatePost", new { text = postDto.Content });
        }

        [HttpDelete("{id}")]
        [DisableRequestSizeLimit]
        [Route("blog/{id}")]
        public async Task<ActionResult<Hero>> DeleteComment(int id)
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