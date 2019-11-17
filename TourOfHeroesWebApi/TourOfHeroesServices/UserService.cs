namespace TourOfHeroesServices
{
    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesDTOs.UserDtos;
    using TourOfHeroesMapping.Mapping;
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using Contracts;

    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IRepository<ApplicationUser> userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
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

            var userRole = GetUserRole(id);

            if (userDetail != null)
            {
                userDetail.Role = userRole;
            }

            return userDetail;
        }

        public async Task UpdateUser(string id, UpdateUserDTO userDTO)
        {
            var userRole = this.GetUserRole(id);

            var user = this._userRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            await this._userManager.RemoveFromRoleAsync(user, userRole);
            await this._userManager.AddToRoleAsync(user, userDTO.Role);
            await this._userRepository.SaveChangesAsync();
        }

        public async Task DeleteUser(string id)
        {
            var userToDelete = this._userRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            this._userRepository.Delete(userToDelete);

            await this._userRepository.SaveChangesAsync();
        }

        private string GetUserRole(string id)
        {
            return this._userManager.GetRolesAsync(_userRepository.All().FirstOrDefault(x => x.Id == id)).Result.FirstOrDefault();
        }
    }
}