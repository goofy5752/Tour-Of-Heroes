namespace TourOfHeroesDTOs
{
    using System.Collections.Generic;
    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    public class GetUserDetailDTO : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string ProfileImage { get; set; }

        public string FullName { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}