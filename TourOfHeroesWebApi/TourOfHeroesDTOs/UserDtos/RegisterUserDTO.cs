namespace TourOfHeroesDTOs.UserDtos
{
    public class RegisterUserDTO
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string JobTitle { get; set; }

        public string EditorRoleCode { get; set; }
    }
}