using System;
using Microsoft.AspNetCore.Http;

namespace TourOfHeroesWebApi.DTOs
{
    public class CreateHeroDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public IFormFile CoverImage { get; set; }

        public string RealName { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }
    }
}
