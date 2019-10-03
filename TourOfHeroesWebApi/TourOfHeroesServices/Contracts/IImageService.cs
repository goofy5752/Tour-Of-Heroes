using Microsoft.AspNetCore.Http;

namespace TourOfHeroesServices.Contracts
{
    public interface IImageService
    {
        string AddToCloudinaryAndReturnImageUrl(IFormFile formFile);
    }
}
