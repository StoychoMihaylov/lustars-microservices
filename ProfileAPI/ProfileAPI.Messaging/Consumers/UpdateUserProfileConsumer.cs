namespace ProfileAPI.Messaging.Consumers
{
    using System.IO;
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using ProfileAPI.Services.Interfaces;
    using ProfileAPI.Models.BidingModels;
    using System.Runtime.Serialization.Formatters.Binary;

    public class UpdateUserProfileConsumer : IConsumer<IUpdateUserProfile>
    {
        private IProfileService profileService;

        public UpdateUserProfileConsumer(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public async Task Consume(ConsumeContext<IUpdateUserProfile> context)
        {
            var message = context.Message;

            var dataModel = DeserializeUserData(message.MessageData);

            var isUpdated = await this.profileService.EditUserProfile(dataModel);

            if (!isUpdated)
            {
                // Message Notification Service Update failed!
            }
            else
            {
                // Message Notification Service Update Success!
            }
        }

        private EditUserProfileBindingModel DeserializeUserData(byte[] message)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(message))
            {
                return (EditUserProfileBindingModel)bf.Deserialize(ms);
            }
        }
    }
}
