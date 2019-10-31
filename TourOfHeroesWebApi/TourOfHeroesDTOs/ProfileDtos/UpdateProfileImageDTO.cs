using Microsoft.AspNetCore.Http;

namespace TourOfHeroesDTOs.ProfileDtos
{
    public class UpdateProfileImageDTO
    {
        public string UserId { get; set; }

        public IFormFile ProfileImage { get; set; }
    }
}