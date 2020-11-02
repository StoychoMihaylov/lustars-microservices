namespace WebGateway.Messaging.Interfaces
{
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using WebGateway.Models.BidingModels.Account;

    public interface IAccountBusService
    {
        Task<(Task<Response<IAccountCredentials>>, Task<Response<IRegisterAccountRejection>>)> MessageAuthAPI_RegisterAccountProfile(RegisterUserBindingModel bm);
        Task MessageAuthAPI_DeleteAccountProfile(Response<IAccountCredentials> credentials);
    }
}
    