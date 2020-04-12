namespace ImageAPI.App.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using ImageAPI.Services.Interfaces;

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
        [Route("{userId}/upload")]
        public IActionResult UploadImage(string userId, [FromForm]IFormFile image)
        {
            var imageUrl = string.Empty;

            try
            {
                imageUrl = this.imageService.SaveImageAsFileAsync(userId, image).Result;
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }

            return StatusCode(200, imageUrl);
        }
    }
}
