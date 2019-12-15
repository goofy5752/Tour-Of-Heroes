namespace TourOfHeroesData.Models
{
    public class UserBlogLikes
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}