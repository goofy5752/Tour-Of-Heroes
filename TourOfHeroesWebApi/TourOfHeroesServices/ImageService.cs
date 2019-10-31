// ReSharper disable IdentifierTypo
namespace TourOfHeroesServices
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Contracts;

    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService()
        {
            Account account = new Account()
            {
                ApiKey = "743681715341912",
                ApiSecret = "LgIgzvDi5vMALb8r3KPKYwLjYlw",
                Cloud = "goofy5752"
            };

            _cloudinary = new Cloudinary(account);
        }

        public string AddToCloudinaryAndReturnHeroImageUrl(IFormFile photo)
        {
            var file = photo;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Folder = "/HeroUploads"
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            var url = uploadResult.Uri.ToString();

            return url;
        }

        public string AddToCloudinaryAndReturnProfileImageUrl(IFormFile photo)
        {
            var file = photo;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Folder = "/ProfilePictures"
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            var url = uploadResult.Uri.ToString();

            return url;
        }

        public string AddToCloudinaryAndReturnBlogImageUrl(IFormFile photo)
        {
            var file = photo;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Folder = "/BlogImages"
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            var url = uploadResult.Uri.ToString();

            return url;
        }
    }
}