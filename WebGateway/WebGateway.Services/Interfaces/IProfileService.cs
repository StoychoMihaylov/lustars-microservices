namespace WebGateway.Services.Interfaces
{
    using System.Threading.Tasks;

    using WebGateway.Models.DTOs;

    public interface IProfileService
    {
        Task<bool> CallProfileAPICreateUserProfile(UserProfile userProfile);
    }
}
