namespace TourOfHeroesDTOs.HeroDtos
{
    using TourOfHeroesData.Models;
    using MovieDtos;
    using TourOfHeroesMapping.Mapping;
    using System.Collections.Generic;

    public class GetHeroDTO : IMapFrom<Hero>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string CoverImage { get; set; }

        public IEnumerable<GetMovieTitleDTO> Movies { get; set; }
    }
}