namespace TourOfHeroesDTOs.BlogDtos
{
    using System;
    using System.Collections.Generic;
    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    public class GetPostDetailDTO : IMapFrom<Blog>
    {
        public GetPostDetailDTO()
        {
            this.Comments = new List<Comment>();
        }

        public int Id { get; set; }

        public string AuthorUserName { get; set; }

        public string Content { get; set; }

        public string BlogImage { get; set; }

        public DateTime PublishedOn { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
