namespace WebGateway.Messaging.MessagingServices
{
    using System;
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using WebGateway.Messaging.Interfaces;
    using WebGateway.Models.BidingModels.Account;

    public class ProfileBusService : IProfileBusService
    {
        private readonly IBus bus;

        public ProfileBusService(IBus bus)
        {
            this.bus = bus;
        }

        public async Task<Response<IUserProfileCreated>> MessageProfileAPI_CreateUserProfile(Response<IAccountCredentials> credentials, RegisterUserBindingModel bm)
        {
            var profileAPI = this.bus.CreateRequestClient<ICreateUserProfile>(new Uri("queue:create-user-profile-queue"), TimeSpan.FromSeconds(30));

            var userResponse = await profileAPI.GetResponse<IUserProfileCreated>(new
            {
                Id = credentials.Message.UserId,
                Name = bm.Name,
                Gender = bm.Gender,
                Email = bm.Email,
            });

            return userResponse;
        }
    }
}
