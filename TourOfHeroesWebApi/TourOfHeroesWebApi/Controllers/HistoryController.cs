namespace TourOfHeroesWebApi.Controllers
{
    using System.Threading.Tasks;

    using TourOfHeroesData.Models;
    using TourOfHeroesServices.Contracts;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class HistoryController : ApiController
    {
        private readonly IHistoryService _historyService;
        private readonly ILoggerManager _logger;

        public HistoryController(IHistoryService historyService, ILoggerManager logger)
        {
            _historyService = historyService;
            _logger = logger;
        }

        #region DeleteHistory

        [HttpDelete("{id}")]
        [DisableRequestSizeLimit]
        [Route("history/{id}")]
        public async Task<ActionResult<Hero>> DeleteHistory(int id)
        {
            var history = this._historyService.GetAllHistories();

            if (history == null)
            {
                return this.NotFound();
            }

            _logger.LogInfo($"Deleting history with id {id}...");

            await this._historyService.DeleteHistory(id);

            _logger.LogInfo($"Successfully deleted history with id {id}...");

            return this.NoContent();
        }

        #endregion
    }
}