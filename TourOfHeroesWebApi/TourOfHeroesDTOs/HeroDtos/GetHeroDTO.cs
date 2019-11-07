namespace TourOfHeroesDTOs.HeroDtos
{
    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    public class GetHeroDTO : IMapFrom<Hero>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RealName { get; set; }

        public string Image { get; set; }
    }
}