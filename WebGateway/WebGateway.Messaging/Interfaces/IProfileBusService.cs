namespace WebGateway.Messaging.Interfaces
{
    using MassTransit;
    using MessageExchangeContract;
    using System.Threading.Tasks;
    using WebGateway.Models.BidingModels.Account;

    public interface IProfileBusService
    {
        Task<Response<IUserProfileCreated>> MessageProfileAPI_CreateUserProfile(Response<IAccountCredentials> credentials, RegisterUserBindingModel bm);
    }
}
