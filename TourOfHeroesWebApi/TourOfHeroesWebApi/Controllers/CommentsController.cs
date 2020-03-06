namespace TourOfHeroesWebApi.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.CommentDtos;
    using TourOfHeroesServices.Contracts;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;
        private readonly ILoggerManager _logger;

        public CommentsController(ICommentService commentService, ILoggerManager logger)
        {
            _commentService = commentService;
            _logger = logger;
        }

        #region CreateComment

        [HttpPost("{create-comment}")]
        [DisableRequestSizeLimit]
        [Route("comments/create-comment")]
        public async Task<ActionResult<CreateCommentDTO>> CreateComment(CreateCommentDTO commentDTO)
        {
            if (!ModelState.IsValid) return this.NoContent();

            _logger.LogInfo("Creating a new comment...");

            await this._commentService.CreateComment(commentDTO);

            _logger.LogInfo($"Comment with content {commentDTO.Comment} successfully created.");

            return this.CreatedAtAction("CreateComment", new { text = commentDTO.Comment });
        }

        #endregion

        #region DeleteComment

        [HttpDelete("{id}")]
        [DisableRequestSizeLimit]
        [Route("comments/{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var currentUserId = HttpContext.User.Claims.First(x => x.Type == "UserID").Value;
            var comment = this._commentService.GetAllComments().FirstOrDefault(x => x.Id == id);

            if (comment == null)
            {
                return this.NotFound("Invalid comment id.");
            }

            if (currentUserId != comment.UserId)
            {
                return this.BadRequest("You can't delete other people comment.");
            }

            _logger.LogInfo($"Deleting comment with id {id}...");

            await this._commentService.DeleteComment(id);

            _logger.LogInfo($"Successfully deleted comment with id {id}...");

            return this.Ok();
        }

        #endregion
    }
}