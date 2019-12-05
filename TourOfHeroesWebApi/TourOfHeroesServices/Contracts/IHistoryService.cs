namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TourOfHeroesData.Models;

    public interface IHistoryService
    {
        IEnumerable<EditHistory> GetAllHistories();

        Task DeleteHistory(int id);
    }
}