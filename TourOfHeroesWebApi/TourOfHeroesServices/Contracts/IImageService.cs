namespace TourOfHeroesServices.Contracts
{
    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        // ReSharper disable once IdentifierTypo
        string AddToCloudinaryAndReturnImageUrl(IFormFile formFile);
    }
}