namespace TourOfHeroesServices.Contracts
{
    using System.Threading.Tasks;

    using TourOfHeroesDTOs.ProfileDtos;

    public interface IProfileService
    {
        GetProfileDetailDTO GetProfile(string id);

        Task UpdateProfileImage(string id, UpdateProfileImageDTO profile, bool skipMethodForTest = false);

        Task UpdateProfileEmail(string id, UpdateProfileEmailDTO email);
    }
}