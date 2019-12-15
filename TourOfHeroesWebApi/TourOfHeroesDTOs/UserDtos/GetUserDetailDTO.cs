namespace TourOfHeroesDTOs.UserDtos
{
    using System.Collections.Generic;

    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    using CommentDtos;

    public class GetUserDetailDTO : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string ProfileImage { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }
    }
}