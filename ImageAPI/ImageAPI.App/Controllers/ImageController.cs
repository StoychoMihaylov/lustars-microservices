namespace ImageAPI.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using ImageAPI.Services.Interfaces;
    using System;

    [Route("image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult UploadImage([FromForm]IFormFile formData)
        {
            try
            {
                var imageUrl = this.imageService.SaveImageAsFileAsync(formData).Result;
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
            
            return StatusCode(200);
        }
    }
}
