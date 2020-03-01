namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TourOfHeroesDTOs.EditHistoryDtos;

    public interface IHistoryService
    {
        IEnumerable<GetAllHistoryDTO> GetAllHistory();

        Task DeleteHistory(int id);
    }
}