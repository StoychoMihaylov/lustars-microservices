namespace AuthAPI.Messaging.Consumers
{
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using AuthAPI.Services.Interfaces;
    using AuthAPI.Models.BidingModels;
    

    public class RegisterNewAccountConsumer : IConsumer<IRegisterNewAccountMessage>
    {
        private readonly IAccountService service;

        public RegisterNewAccountConsumer(IAccountService service)
        {
            this.service = service;
        }


        public async Task Consume(ConsumeContext<IRegisterNewAccountMessage> context)
        {
            var message = context.Message;

            var bm = new RegisterUserBindingModel()
            {
                Name = message.Name,
                Email = message.Email,
                Password = message.Password,
                ConfirmPassword = message.ConfirmPassword
            };

            var userCredentials = this.service.CreateNewUserAccount(bm); // User created, will return token(loged-in automaticaly)
        }
    }
}
