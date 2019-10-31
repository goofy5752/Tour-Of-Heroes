using TourOfHeroesData.Models;
using TourOfHeroesMapping.Mapping;

namespace TourOfHeroesDTOs.MovieDtos
{
    public class GetMovieTitleDTO : IMapFrom<Movie>
    {
        public string Title { get; set; }
    }
}