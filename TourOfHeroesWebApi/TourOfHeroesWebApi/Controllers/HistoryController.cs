using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourOfHeroesData.Models;
using TourOfHeroesServices.Contracts;

namespace TourOfHeroesWebApi.Controllers
{
    public class HistoryController : ApiController
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpDelete("{id}"), DisableRequestSizeLimit]
        [Route("history/{id}")]
        public async Task<ActionResult<Hero>> DeleteHistory(int id)
        {
            var history = this._historyService.GetAllHistories();

            if (history == null)
            {
                return this.NotFound();
            }

            await this._historyService.DeleteHistory(id);
            return this.NoContent();
        }
    }
}
