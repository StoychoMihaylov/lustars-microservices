namespace ProfileAPI.Messaging.Consumers
{
    using System;
    using MassTransit;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using ProfileAPI.Services.Interfaces;
    using ProfileAPI.Models.BidingModels;
    using ProfileAPI.Messaging.Interfaces;

    public class UpdateUserProfileConsumer : IConsumer<IUpdateUserProfile>
    {
        private readonly IProfileService profileService;
        private readonly INotificationBusService notificationBusService;

        public UpdateUserProfileConsumer(IProfileService profileService, INotificationBusService notificationBusService)
        {
            this.profileService = profileService;
            this.notificationBusService = notificationBusService;
        }

        public async Task Consume(ConsumeContext<IUpdateUserProfile> context)
        {
            var message = context.Message.MessageData;
            if (message.HasValue)
            {
                var json = await message.Value;

                var updateUserBm = JsonConvert.DeserializeObject<EditUserProfileBindingModel>(json);

                var isUpdated = await this.profileService.EditUserProfile(updateUserBm); // Languages update logic needs to be fixed
                if (isUpdated)
                {
                    this.notificationBusService.SendMessageToNotificationAPI("Profile updated successfully!");
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
    }
}
