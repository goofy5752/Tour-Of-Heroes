using System.Collections.Generic;
using System.Threading.Tasks;
using TourOfHeroesData.Models;

namespace TourOfHeroesServices.Contracts
{
    public interface IHistoryService
    {
        IEnumerable<EditHistory> GetAllHistories();

        Task DeleteHistory(int id);
    }
}
