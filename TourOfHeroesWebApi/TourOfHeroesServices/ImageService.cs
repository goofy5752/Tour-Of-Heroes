using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using TourOfHeroesData;
using TourOfHeroesServices.Contracts;

namespace TourOfHeroesServices
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly TourOfHeroesDbContext _context;

        public ImageService(TourOfHeroesDbContext context)
        {
            this._context = context;
            Account account = new Account()
            {
                ApiKey = "743681715341912",
                ApiSecret = "LgIgzvDi5vMALb8r3KPKYwLjYlw",
                Cloud = "goofy5752"
            };

            _cloudinary = new Cloudinary(account);
        }

        public string AddToCloudinaryAndReturnImageUrl(IFormFile photo)
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

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
