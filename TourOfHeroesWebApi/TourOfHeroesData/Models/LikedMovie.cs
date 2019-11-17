namespace TourOfHeroesData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LikedMovie
    {
        public int Id { get; set; }

        [MaxLength(5000)]
        public string Title { get; set; }

        [MaxLength(300)]
        public string PosterPath { get; set; }

        public int VoteCount { get; set; }

        public int VoteAverage { get; set; }

        public DateTime ReleaseDate { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}