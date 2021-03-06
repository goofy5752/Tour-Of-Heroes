﻿namespace TourOfHeroesServices
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.UserDtos;
    using TourOfHeroesMapping.Mapping;

    using Contracts;

    using Microsoft.AspNetCore.Identity;

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

        public GetUserDetailDTO GetUserDetail(string id, bool skipMethodForTest = false)
        {
            var userDetail = this._userRepository
                .All()
                .To<GetUserDetailDTO>()
                .Single(x => x.Id == id);

            if (!skipMethodForTest)
            {
                var userRole = GetUserRole(id);

                userDetail.Role = userRole;
            }

            return userDetail;
        }

        public async Task UpdateUser(string id, UpdateUserDTO userDTO, bool skipMethodForTest = false)
        {
            var user = this._userRepository
                .All()
                .Single(x => x.Id == id);

            var roles = new []{"Admin", "User", "Editor"};

            if (!roles.Contains(userDTO.Role))
            {
                throw new InvalidOperationException("Role doesn't exist.");
            }

            var userRole = "";

            if (!skipMethodForTest)
            {
                userRole = this.GetUserRole(id);
            }

            await this._userManager.RemoveFromRoleAsync(user, userRole);
            await this._userManager.AddToRoleAsync(user, userDTO.Role);
            await this._userRepository.SaveChangesAsync();
        }

        public async Task DeleteUser(string id)
        {
            var userToDelete = this._userRepository
                .All()
                .Single(x => x.Id == id);

            this._userRepository.Delete(userToDelete);

            await this._userRepository.SaveChangesAsync();
        }

        private string GetUserRole(string id)
        {
            return this._userManager.GetRolesAsync(_userRepository.All().FirstOrDefault(x => x.Id == id)).Result.FirstOrDefault();
        }
    }
}