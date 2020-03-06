namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TourOfHeroesDTOs.UserDtos;

    public interface IUserService
    {
        IEnumerable<GetUserDTO> GetAllUsers();

        GetUserDetailDTO GetUserDetail(string id, bool skipMethodForTest = false);

        Task UpdateUser(string id, UpdateUserDTO userDTO, bool skipMethodForTest = false);

        Task DeleteUser(string id);
    }
}