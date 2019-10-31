using System.Collections.Generic;
using TourOfHeroesData.Models;
using TourOfHeroesDTOs.MovieDtos;
using TourOfHeroesMapping.Mapping;

namespace TourOfHeroesDTOs.HeroDtos
{
    public class GetHeroDTO : IMapFrom<Hero>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string CoverImage { get; set; }

        public IEnumerable<GetMovieTitleDTO> Movies { get; set; }
    }
}