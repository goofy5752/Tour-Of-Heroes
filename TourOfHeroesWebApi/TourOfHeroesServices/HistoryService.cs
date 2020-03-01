namespace TourOfHeroesServices
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TourOfHeroesData.Models;
    using TourOfHeroesMapping.Mapping;
    using TourOfHeroesDTOs.EditHistoryDtos;
    using TourOfHeroesData.Common.Contracts;

    using Contracts;

    public class HistoryService : IHistoryService
    {
        private readonly IRepository<EditHistory> _editHistoryRepository;

        public HistoryService(IRepository<EditHistory> editHistoryRepository)
        {
            _editHistoryRepository = editHistoryRepository;
        }
        
        public IEnumerable<GetAllHistoryDTO> GetAllHistory()
        {
            return this._editHistoryRepository
                .All()
                .To<GetAllHistoryDTO>()
                .ToList();
        }

        public async Task DeleteHistory(int id)
        {
            var historyToDelete = this._editHistoryRepository.All().Single(x => x.Id == id);

            this._editHistoryRepository.Delete(historyToDelete);

            await this._editHistoryRepository.SaveChangesAsync();
        }
    }
}