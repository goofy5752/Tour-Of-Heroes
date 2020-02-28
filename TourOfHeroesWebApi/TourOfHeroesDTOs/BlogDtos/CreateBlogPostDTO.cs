namespace TourOfHeroesDTOs.BlogDtos
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateBlogPostDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        public string Content { get; set; }

        [Required]
        public IFormFile BlogImage { get; set; }
    }
}