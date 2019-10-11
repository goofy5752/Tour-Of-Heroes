namespace TourOfHeroesData.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual Hero Hero { get; set; }
        public int HeroId { get; set; }
    }
}