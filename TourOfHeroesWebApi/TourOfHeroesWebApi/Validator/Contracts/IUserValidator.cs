namespace TourOfHeroesWebApi.Validator.Contracts
{
    using System.Threading.Tasks;

    public interface IUserValidator
    {
        Task<bool> CheckPasswordAsync(string password);
    }
}