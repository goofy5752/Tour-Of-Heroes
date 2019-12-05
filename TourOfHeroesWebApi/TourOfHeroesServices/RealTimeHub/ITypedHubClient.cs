namespace TourOfHeroesServices.RealTimeHub
{
    using System.Threading.Tasks;

    using TourOfHeroesData.Models;

    public interface ITypedHubClient
    {
        Task BroadcastComment(Comment comment);

        Task DeleteComment(int commentId);

        Task UpdateProfileImage(string profileImage);
    }
}