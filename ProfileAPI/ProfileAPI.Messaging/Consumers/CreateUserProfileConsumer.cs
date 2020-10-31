namespace ProfileAPI.Messaging.Consumers
{
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using ProfileAPI.Models.BidingModels;
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

            var bm = new CreateUserProfileBindingModel()
            {
                Id = message.Id,
                Name = message.Name,
                Gender = message.Gender,
                Email = message.Email,
            };

            var isCreated = this.profileService.CreateNewUserProfile(bm);

            await context.RespondAsync<IUserProfileCreated>(new
            {
                isCreated = isCreated
            }); ;
        }
    }
}
