namespace AuthAPI.Messaging.Consumers
{
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using AuthAPI.Services.Interfaces;
    using AuthAPI.Models.BidingModels;

    public class RegisterAccountConsumer : IConsumer<IRegisterAccountProfile>
    {
        private readonly IAccountService service;

        public RegisterAccountConsumer(IAccountService service)
        {
            this.service = service;
        }

        public async Task Consume(ConsumeContext<IRegisterAccountProfile> context)
        {
            var message = context.Message;

            var bm = new RegisterUserBindingModel()
            {
                Name = message.Name,
                Email = message.Email,
                Password = message.Password,
                ConfirmPassword = message.ConfirmPassword
            };

            var userAlreadyExist = await this.service.CheckIfUserExist(bm);
            if (userAlreadyExist)
            {
                await context.RespondAsync<IRegisterAccountRejection>(new
                {
                    Value = "User with this email already exist!"
                });
            }
            else
            {
                var userCredentials = await this.service.CreateNewUserAccount(bm); // User created, will return token(loged-in automaticaly)

                await context.RespondAsync<IAccountCredentials>(userCredentials);
            }
        }  
    }
}
