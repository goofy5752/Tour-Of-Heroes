namespace TourOfHeroesDTOs.UserDtos
{
    using System;
    using System.Collections.Generic;

    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    using BlogDtos;
    using CommentDtos;

    public class GetUserDTO : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string JobTitle { get; set; }

        public string ProfileImage { get; set; }

        public DateTime RegisteredOn { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; }

        public IEnumerable<GetPostDetailDTO> Blogs { get; set; }
    }
}