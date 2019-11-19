namespace TourOfHeroesDTOs.ProfileDtos
{
    using CommentDtos;
    using System.Collections.Generic;
    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    public class GetProfileDetailDTO : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string ProfileImage { get; set; }

        public string FullName { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; }

        public IEnumerable<Blog> Blogs { get; set; }

        public IEnumerable<LikedMovie> LikedMovies { get; set; }
    }
}