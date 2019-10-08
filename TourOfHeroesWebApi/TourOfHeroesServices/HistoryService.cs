namespace TourOfHeroesServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using Contracts;
    using System.Linq;

    public class HistoryService : IHistoryService
    {
        private readonly IRepository<EditHistory> _editHistoryRepository;

        public HistoryService(IRepository<EditHistory> editHistoryRepository)
        {
            _editHistoryRepository = editHistoryRepository;
        }
        
        public IEnumerable<EditHistory> GetAllHistories()
        {
            return this._editHistoryRepository.All().ToList();
        }

        public async Task DeleteHistory(int id)
        {
            var historyToDelete = this._editHistoryRepository.All().FirstOrDefault(x => x.Id == id);

            this._editHistoryRepository.Delete(historyToDelete);

            await this._editHistoryRepository.SaveChangesAsync();
        }
    }
}