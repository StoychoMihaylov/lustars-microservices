namespace WebGateway.Messaging.MessagingServices
{
    using System;
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using WebGateway.Messaging.Interfaces;
    using WebGateway.Models.BidingModels.Account;

    public class AccountBusService : IAccountBusService
    {
        private readonly IBus bus;

        public AccountBusService(IBus bus)
        {
            this.bus = bus;
        }

        public async Task MessageAuthAPI_DeleteAccountProfile(Response<IAccountCredentials> credentials)
        {
            var userId = credentials.Message.UserId;

            var endPoint = await this.bus.GetSendEndpoint(new Uri("queue:delete-account-profile-queue"));

            endPoint.Send<IDeleteAccountProfile>(new { Id = userId });
        }

        public async Task<(Task<Response<IAccountCredentials>>, Task<Response<IRegisterAccountRejection>>)> MessageAuthAPI_RegisterAccountProfile(RegisterUserBindingModel bm)
        {
            var authAPI = this.bus.CreateRequestClient<IRegisterAccountProfile>(new Uri("queue:register-account-profile-queue"), TimeSpan.FromSeconds(30));

            return await authAPI.GetResponse<IAccountCredentials, IRegisterAccountRejection>(bm);
        }
    }
}
