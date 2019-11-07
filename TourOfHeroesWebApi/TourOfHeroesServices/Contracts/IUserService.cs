namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesDTOs.UserDtos;

    public interface IUserService
    {
        IEnumerable<GetUserDTO> GetAllUsers();

        GetUserDetailDTO GetUserDetail(string id);

        Task UpdateUser(string id, UpdateUserDTO userDTO);

        Task DeleteUser(string id);
    }
}