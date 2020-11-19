namespace ProfileAPI.Messaging.Consumers
{
    using System;
    using MassTransit;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using ProfileAPI.Services.Interfaces;
    using ProfileAPI.Models.BidingModels;

    public class UpdateUserProfileConsumer : IConsumer<IUpdateUserProfile>
    {
        private IProfileService profileService;

        public UpdateUserProfileConsumer(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public async Task Consume(ConsumeContext<IUpdateUserProfile> context)
        {
            var message = context.Message.MessageData;
            if (message.HasValue)
            {
                var data = await message.Value;
                Console.WriteLine(data);
                var updateUserBm = DeserializeJSONtoObject(data);
                var isUpdated = await this.profileService.EditUserProfile(updateUserBm); // Languages update logic needs to be fixed
                if (isUpdated)
                {
                    Console.WriteLine("PROFILE UPDATED!");
                    // Message Notification Service Update Success!
                }
                else
                {
                    Console.WriteLine("PROFILE FAILD TO UPDATE");
                    
                    // Message Notification Service Update failed!
                }
            }
            else
            {
                // Message Notification Service Update failed!
            }
        }

        private EditUserProfileBindingModel DeserializeJSONtoObject(dynamic json)
        {
            var obj = JsonConvert.DeserializeObject<EditUserProfileBindingModel>(json);

            return obj;
        }
    }
}
