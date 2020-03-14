namespace ImageAPI.Services.Interfaces
{
    using System.Threading.Tasks;

    public interface IImageService
    {
        Task<string> SaveImageAsFileAsync(Microsoft.AspNetCore.Http.IFormFile formData);
    }
}
