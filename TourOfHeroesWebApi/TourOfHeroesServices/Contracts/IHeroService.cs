namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesDTOs.HeroDtos;

    public interface IHeroService
    {
        IEnumerable<GetHeroDTO> GetAllHeroes();

        GetHeroDetailDTO GetById(int id);

        IEnumerable<GetHeroDTO> GetHeroBySearchString(string name);

        Task CreateHero(CreateHeroDTO hero);

        Task UpdateHero(int id, UpdateHeroDTO hero);

        Task DeleteHero(int id);
    }
}