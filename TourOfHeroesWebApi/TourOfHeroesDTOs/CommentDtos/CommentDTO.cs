namespace TourOfHeroesDTOs.CommentDtos
{
    using System;

    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    public class CommentDTO : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string UserName { get; set; }

        public string ProfileImage { get; set; }

        public DateTime PublishedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int? HeroId { get; set; }
        public virtual Hero Hero { get; set; }

        public string UserId { get; set; }

        public int? BlogId { get; set; }
    }
}