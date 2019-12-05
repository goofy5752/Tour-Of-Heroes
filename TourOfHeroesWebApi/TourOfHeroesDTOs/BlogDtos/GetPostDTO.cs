namespace TourOfHeroesDTOs.BlogDtos
{
    using System;

    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    public class GetPostDTO : IMapFrom<Blog>
    {
        public int Id { get; set; }

        public string AuthorUserName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }

        public string BlogImage { get; set; }
    }
}