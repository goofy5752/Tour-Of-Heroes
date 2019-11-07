namespace TourOfHeroesServices
{
    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesDTOs.UserDtos;
    using TourOfHeroesMapping.Mapping;
    using System.Linq;
    using Contracts;

    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> _userRepository;

        public UserService(IRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<GetUserDTO> GetAllUsers()
        {
            var users = this._userRepository
                .All()
                .To<GetUserDTO>()
                .ToList();

            return users;
        }

        public GetUserDetailDTO GetUserDetail(string id)
        {
            var userDetail = this._userRepository
                .All()
                .To<GetUserDetailDTO>()
                .FirstOrDefault(x => x.Id == id);

            return userDetail;
        }

        //TODO: Implement logic for user role change
        public Task UpdateUser(string id, UpdateUserDTO userDTO)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteUser(string id)
        {
            var userToDelete = this._userRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            this._userRepository.Delete(userToDelete);

            await this._userRepository.SaveChangesAsync();
        }
    }
}