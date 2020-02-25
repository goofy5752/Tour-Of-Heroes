namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TourOfHeroesDTOs.HeroDtos;

    public interface IHeroService
    {
        IEnumerable<GetHeroDTO> GetAllHeroes();

        GetHeroDetailDTO GetById(string currentUser, int id);

        IEnumerable<GetHeroDTO> GetHeroBySearchString(string name);

        Task CreateHero(CreateHeroDTO hero, bool skipAddToCloudinaryMethod = false);

        Task UpdateHero(int id, UpdateHeroDTO hero);

        Task DeleteHero(int id);
    }
}