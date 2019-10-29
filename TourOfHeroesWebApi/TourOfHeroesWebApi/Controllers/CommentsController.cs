namespace TourOfHeroesWebApi.Controllers
{
    using System;
    using TourOfHeroesDTOs;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TourOfHeroesData.Models;
    using TourOfHeroesServices.Contracts;
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

        [HttpDelete("{id}")]
        [DisableRequestSizeLimit]
        [Route("comments/{id}")]
        public async Task<ActionResult<Hero>> DeleteComment(int id)
        {
            var history = this._commentService.GetAllComments();

            if (history == null)
            {
                return this.NotFound();
            }

            _logger.LogInfo($"Deleting comment with id {id}...");

            await this._commentService.DeleteComment(id);

            _logger.LogInfo($"Successfully deleted comment with id {id}...");

            return this.NoContent();
        }
    }
}