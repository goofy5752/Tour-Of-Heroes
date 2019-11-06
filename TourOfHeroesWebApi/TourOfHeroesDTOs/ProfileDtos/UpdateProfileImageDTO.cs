namespace TourOfHeroesDTOs.ProfileDtos
{
    using Microsoft.AspNetCore.Http;

    public class UpdateProfileImageDTO
    {
        public string UserId { get; set; }

        public IFormFile ProfileImage { get; set; }
    }
}