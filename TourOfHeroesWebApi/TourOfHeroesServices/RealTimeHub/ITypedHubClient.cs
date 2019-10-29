namespace TourOfHeroesServices.RealTimeHub
{
    using TourOfHeroesData.Models;
    using System.Threading.Tasks;

    public interface ITypedHubClient
    {
        Task BroadcastComment(Comment comment);
    }
}