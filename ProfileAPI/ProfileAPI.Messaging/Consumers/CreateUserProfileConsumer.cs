namespace ProfileAPI.Messaging.Consumers
{
    using System;
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using ProfileAPI.Data.Entities;
    using ProfileAPI.Services.Interfaces;

    public class CreateUserProfileConsumer : IConsumer<ICreateUserProfile>
    {
        private IProfileService profileService;

        public CreateUserProfileConsumer(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public async Task Consume(ConsumeContext<ICreateUserProfile> context)
        {
            var message = context.Message;

            UserProfile newProfile = new UserProfile()
            {
                Id = message.Id,
                Name = message.Name,
                Gender = message.Gender,
                Email = message.Email,
                CreatedOn = DateTime.UtcNow
            };

            var isCreated = await this.profileService.CreateNewUserProfile(newProfile);

            await context.RespondAsync<IUserProfileCreated>(new
            {
                isCreated = isCreated
            });
        }
    }
}
