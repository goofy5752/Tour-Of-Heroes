namespace TourOfHeroesDTOs
{
    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    public class GetMovieTitleDTO : IMapFrom<Movie>
    {
        public string Title { get; set; }
    }
}