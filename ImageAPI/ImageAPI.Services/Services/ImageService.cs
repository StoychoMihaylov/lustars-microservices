namespace ImageAPI.Services.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;

    using ImageAPI.Services.Interfaces;

    public class ImageService : IImageService
    {
        private IHostingEnvironment env;

        public ImageService(IHostingEnvironment env)
        {
            this.env = env;
        }

        public async Task<string> SaveImageAsFileAsync(string userId, IFormFile image)
        {
            var imgNameUrl = string.Empty;
            
            try
            {
                string path = Path.Combine(this.env.ContentRootPath + $"/wwwroot/images/{userId}");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var imgName = Guid.NewGuid().ToString() + ".jpeg";
                imgNameUrl = userId + "/" + imgName;

                using (var img = new FileStream(Path.Combine(path, imgName), FileMode.Create))
                {
                    await image.CopyToAsync(img);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return imgNameUrl;
        }
    }
}
