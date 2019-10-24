namespace TourOfHeroesServices.Contracts
{
    using System.Threading.Tasks;
    using TourOfHeroesDTOs;

    public interface IProfileService
    {
        GetUserDetailDTO GetUser(string id);

        Task UpdateProfileImage(string id, UpdateProfileImageDTO profile);

        Task UpdateProfileEmail(string id, UpdateProfileEmailDTO email);
    }
}