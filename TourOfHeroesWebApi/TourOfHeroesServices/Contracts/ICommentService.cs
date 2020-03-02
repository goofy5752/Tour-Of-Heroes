namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.CommentDtos;

    public interface ICommentService
    {
        IEnumerable<Comment> GetAllComments();

        Task CreateComment(CreateCommentDTO commentDto, bool skipMethodForTest = false);

        Task DeleteComment(int id, bool skipMethodForTest = false);
    }
}