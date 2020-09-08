namespace TourOfHeroesDTOs.BlogDtos
{
    using Microsoft.AspNetCore.Http;

    public class EditBlogPostDTO
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public IFormFile BlogImage { get; set; }
    }
}