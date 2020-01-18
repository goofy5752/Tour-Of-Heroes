namespace TourOfHeroesDTOs.BlogDtos
{
    using System;
    using System.Collections.Generic;

    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    using CommentDtos;

    public class GetLatestPostsDTO : IMapFrom<Blog>
    {
        public int Id { get; set; }

        public string AuthorUserName { get; set; }

        public string Title { get; set; }

        public DateTime PublishedOn { get; set; }

        public string BlogImage { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; }
    }
}