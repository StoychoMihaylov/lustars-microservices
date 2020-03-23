namespace ImageAPI.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        Task<string> SaveImageAsFileAsync(string userId, IFormFile formData);
    }
}
