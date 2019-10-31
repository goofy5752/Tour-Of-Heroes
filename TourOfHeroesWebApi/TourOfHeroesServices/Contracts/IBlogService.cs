namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.BlogDtos;

    public interface IBlogService
    {
        IEnumerable<Blog> GetAllPosts();

        Task CreatePost(CreateBlogPostDTO postDto);

        Task DeletePost(int id);
    }
}