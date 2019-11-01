namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.BlogDtos;

    public interface IBlogService
    {
        IEnumerable<GetPostDTO> GetAllPosts();

        GetPostDetailDTO GetPostDetail(int id);

        Task CreatePost(CreateBlogPostDTO postDto, string userId);

        Task DeletePost(int id);
    }
}