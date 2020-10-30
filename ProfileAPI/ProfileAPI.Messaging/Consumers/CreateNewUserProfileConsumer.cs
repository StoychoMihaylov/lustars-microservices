namespace ProfileAPI.Messaging.Consumers
{
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using ProfileAPI.Models.BidingModels;

    public class CreateNewUserProfileConsumer : IConsumer<ICreateNewUserProfile>
    {
        public Task Consume(ConsumeContext<ICreateNewUserProfile> context)
        {
            var message = context.Message;

            var bm = new CreateUserProfileBindingModel()
            { 
                Id = message.Id,
                Name = message.Name,
                Gender = message.Gender,
                Email = message.Email,
            };

            // TO DO: Create ne User Profile Async

            // TO DO: Return response to the WebGateway API
        }
    }
}
