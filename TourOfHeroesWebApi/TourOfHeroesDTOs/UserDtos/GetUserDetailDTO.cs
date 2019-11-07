﻿namespace TourOfHeroesDTOs.UserDtos
{
    using System.Collections.Generic;
    using TourOfHeroesData.Models;
    using CommentDtos;

    public class GetUserDetailDTO
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string ProfileImage { get; set; }

        public string FullName { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; }

        public IEnumerable<Blog> Blogs { get; set; }
    }
}