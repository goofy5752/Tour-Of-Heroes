using System.Collections.Generic;
using System.Threading.Tasks;
using TourOfHeroesData.Models;
using TourOfHeroesDTOs;

namespace TourOfHeroesServices.Contracts
{
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
