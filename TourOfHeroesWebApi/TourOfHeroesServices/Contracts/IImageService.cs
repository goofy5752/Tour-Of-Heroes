namespace TourOfHeroesServices.Contracts
{
    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        string AddToCloudinaryAndReturnHeroImageUrl(IFormFile formFile);

        string AddToCloudinaryAndReturnProfileImageUrl(IFormFile photo);
    }
}