namespace TourOfHeroesDTOs
{
    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    // ReSharper disable once InconsistentNaming
    public class GetHeroDTO : IMapFrom<Hero>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string CoverImage { get; set; }
    }
}