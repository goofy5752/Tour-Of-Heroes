namespace TourOfHeroesData.Models
{
    public class UserActivity
    {
        public int Id { get; set; }

        public string Action { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}