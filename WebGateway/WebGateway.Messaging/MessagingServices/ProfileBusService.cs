namespace WebGateway.Messaging.MessagingServices
{
    using System;
    using System.IO;
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using WebGateway.Messaging.Interfaces;
    using WebGateway.Models.BidingModels.Account;
    using WebGateway.Models.BidingModels.UserProfile;
    using System.Runtime.Serialization.Formatters.Binary;

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

        public async void MessageProfileAPI_UpdateUserProfile(UserProfileBindingModel bm)
        {
            var byteArrayData = SerializeUserData(bm);

            var endpoint = await this.bus.GetSendEndpoint(new Uri("queue:update-user-profile-queue"));
            await endpoint.Send<IUpdateUserProfile>(new
            {
                MessageData = byteArrayData
            });
        }

        private byte[] SerializeUserData(UserProfileBindingModel obj)
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
