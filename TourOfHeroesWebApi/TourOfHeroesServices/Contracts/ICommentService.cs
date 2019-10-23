namespace TourOfHeroesServices.Contracts
{
    using TourOfHeroesDTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesData.Models;

    public interface ICommentService
    {
        IEnumerable<Comment> GetAllComments();

        Task CreateComment(CreateCommentDTO commentDTO);

        Task DeleteComment(int id);
    }
}