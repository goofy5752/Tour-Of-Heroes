namespace TourOfHeroesDTOs.BlogDtos
{
    using System;
    using System.Collections.Generic;

    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    using CommentDtos;

    public class GetPostDetailDTO : IMapFrom<Blog>
    {
        public GetPostDetailDTO()
        {
            this.Comments = new List<CommentDTO>();
        }

        public int Id { get; set; }

        public string AuthorUserName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string BlogImage { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public string CurrentUser { get; set; }

        public DateTime PublishedOn { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; }
    }
}