namespace TourOfHeroesWebApi.Controllers.Validator.Contracts
{
    using System.Threading.Tasks;

    public interface IUserValidator
    {
        Task<bool> CheckPasswordAsync(string userId, string password);
    }
}