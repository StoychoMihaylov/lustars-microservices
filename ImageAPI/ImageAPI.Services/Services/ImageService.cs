namespace ImageAPI.Services.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using ImageAPI.Services.Interfaces;


    class ImageService : IImageService
    {
        private IHostingEnvironment env;

        public ImageService(IHostingEnvironment env)
        {
            this.env = env;
        }

        public async Task<string> SaveImageAsFileAsync(IFormFile formData)
        {
            var imgUrl = string.Empty;

            try
            {
                string path = Path.Combine(this.env.ContentRootPath + "\\wwwroot\\Images");
                var newImgName = Guid.NewGuid().ToString() + (formData.FileName.Substring(formData.FileName.LastIndexOf('.')));
                imgUrl = path + newImgName;
                using (var img = new FileStream(Path.Combine(path, newImgName), FileMode.Create))
                {
                    await formData.CopyToAsync(img);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return imgUrl;
        }
    }
}
