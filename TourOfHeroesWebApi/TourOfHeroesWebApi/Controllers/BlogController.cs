namespace TourOfHeroesWebApi.Controllers
{
    using System;
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
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            _logger.LogInfo($"Fetching blog with id {id}...");

            var detail = this._blogService.GetPostDetail(userId, id);

            _logger.LogInfo($"Blog with id {id} successfully fetched.");

            return detail;
        }

        #endregion

        #region CreatePost

        [HttpPost]
        [DisableRequestSizeLimit]
        [Route("create-post")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<ActionResult<CreateBlogPostDTO>> CreatePost([FromForm] CreateBlogPostDTO postDto)
        {
            var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

            if (!ModelState.IsValid) return this.NoContent();

            _logger.LogInfo("Creating a new post...");

            await this._blogService.CreatePost(postDto, userId);

            _logger.LogInfo($"Post with content {postDto.Content} successfully created.");

            return this.CreatedAtAction("CreatePost", new { text = postDto.Content });
        }

        #endregion

        #region EditPost

        [HttpPut]
        [DisableRequestSizeLimit]
        [Route("edit-post")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<ActionResult<EditBlogPostDTO>> EditPost([FromForm] EditBlogPostDTO postDto)
        {
            try
            {
                if (!ModelState.IsValid) return this.NoContent();

                _logger.LogInfo($"Editing a post with id {postDto.Id}...");

                await this._blogService.EditPost(postDto);

                _logger.LogInfo($"Post with id {postDto.Id} successfully edited.");

                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        #endregion

        #region LikePost

        [HttpPost("{id}")]
        [DisableRequestSizeLimit]
        [Route("blog/{id}")]
        public async Task<ActionResult<Blog>> LikePost(int id)
        {
            try
            {
                var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

                _logger.LogInfo($"Liked post with id {id} ...");

                await this._blogService.LikePost(userId, id);

                _logger.LogInfo($"Successfully liked post with id {id}...");

                return this.Ok();
            }
            catch (Exception)
            {
                return this.BadRequest("You have already like this post.");
            }
        }

        #endregion

        #region DislikePost

        [HttpPost]
        [DisableRequestSizeLimit]
        [Route("dislike")]
        public async Task<ActionResult<Blog>> DislikePost([FromForm]string id)
        {
            try
            {
                var userId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;

                _logger.LogInfo($"Disliked post with id {id} ...");

                await this._blogService.DislikePost(userId, int.Parse(id));

                _logger.LogInfo($"Successfully disliked post with id {id}...");

                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
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

            var postDetail = this._blogService.GetPostDetail(userId, id);

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