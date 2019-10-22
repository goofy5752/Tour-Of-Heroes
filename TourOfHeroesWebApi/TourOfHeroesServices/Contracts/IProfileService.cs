namespace TourOfHeroesServices.Contracts
{
    using System.Threading.Tasks;
    using TourOfHeroesDTOs;

    public interface IProfileService
    {
        GetUserDetailDTO GetUser(string id);

        Task UpdateUser(string id, UpdateUserDTO user);
    }
}