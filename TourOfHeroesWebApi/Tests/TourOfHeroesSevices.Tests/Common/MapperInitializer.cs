namespace TourOfHeroesServices.Tests.Common
{
    using System.Reflection;

    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.UserDtos;
    using TourOfHeroesMapping.Mapping;

    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(GetUserDetailDTO).GetTypeInfo().Assembly,
                typeof(ApplicationUser).GetTypeInfo().Assembly);
        }
    }
}