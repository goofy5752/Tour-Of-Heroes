namespace TourOfHeroesDTOs.MovieDtos
{
    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    public class GetLikedMovieDTO : IMapFrom<LikedMovie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterPath { get; set; }

        public string VoteCount { get; set; }

        public string VoteAverage { get; set; }

        public string ReleaseDate { get; set; }
    }
}