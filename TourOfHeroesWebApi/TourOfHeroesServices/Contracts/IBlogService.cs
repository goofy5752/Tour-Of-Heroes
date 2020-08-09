namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TourOfHeroesDTOs.BlogDtos;

    public interface IBlogService
    {
        IEnumerable<GetPostDTO> GetAllPosts();

        GetPostDetailDTO GetPostDetail(string currentUser, int id);

        Task LikePost(string userId, int blogId);

        Task DislikePost(string userId, int blogId);

        Task CreatePost(CreateBlogPostDTO postDto, string userId, bool skipMethodForTest = false);

        Task EditPost(EditBlogPostDTO postDto, bool skipMethodForTest = false);

        Task DeletePost(int id);
    }
}