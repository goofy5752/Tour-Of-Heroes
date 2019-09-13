using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TourOfHeroesServices.Contracts
{
    public interface IImageService
    {
        string AddToCloudinaryAndReturnImageUrl(IFormFile formFile);

        Task<bool> SaveAllAsync();
    }
}
