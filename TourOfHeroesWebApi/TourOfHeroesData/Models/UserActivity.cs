namespace TourOfHeroesData.Models
{
    using System;

    public class UserActivity
    {
        public int Id { get; set; }

        public string Action { get; set; }

        public DateTime RegisteredOn { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}