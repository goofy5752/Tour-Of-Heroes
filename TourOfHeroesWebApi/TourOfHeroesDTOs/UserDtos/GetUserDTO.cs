namespace TourOfHeroesDTOs.UserDtos
{
    using System;
    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;
    using System.Collections.Generic;
    using CommentDtos;

    public class GetUserDTO : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string ProfileImage { get; set; }

        public DateTime RegisteredOn { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; }

        public IEnumerable<Blog> Blogs { get; set; }
    }
}