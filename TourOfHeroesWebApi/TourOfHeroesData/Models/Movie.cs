namespace TourOfHeroesData.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Title { get; set; }

        public virtual Hero Hero { get; set; }
        public int HeroId { get; set; }
    }
}