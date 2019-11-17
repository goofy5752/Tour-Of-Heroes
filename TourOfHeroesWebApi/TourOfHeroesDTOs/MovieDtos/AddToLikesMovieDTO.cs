namespace TourOfHeroesDTOs.MovieDtos
{
    using System;

    public class AddToLikesMovieDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterPath { get; set; }

        public int VoteCount { get; set; }

        public int VoteAverage { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string UserId { get; set; }
    }
}