namespace TourOfHeroesDTOs.UserDtos
{
    using System;

    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;

    public class UserActivityDTO : IMapFrom<UserActivity>
    {
        public string Action { get; set; }

        public DateTime RegisteredOn { get; set; }
    }
}