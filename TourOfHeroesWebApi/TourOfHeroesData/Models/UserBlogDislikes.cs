namespace TourOfHeroesData.Models
{
    public class UserBlogDislikes
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}