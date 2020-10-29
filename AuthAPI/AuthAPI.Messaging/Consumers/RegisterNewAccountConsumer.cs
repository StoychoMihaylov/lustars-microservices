namespace AuthAPI.Messaging.Consumers
{
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using AuthAPI.Services.Interfaces;
    using AuthAPI.Models.BidingModels;
    using AuthAPI.Models.ViewModels;

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

            var userAlreadyExist = await this.service.CheckIfUserExist(bm);
            if (userAlreadyExist)
            {
                await context.RespondAsync<IRegisterNewAccountRejection>(new
                {
                    Value = "User with this email already exist!"
                });
            }

            var userCredentials = await this.service.CreateNewUserAccount(bm); // User created, will return token(loged-in automaticaly)
            if (userCredentials.GetType().Equals(typeof(AccountCredentialsViewModel)))
            {
                await context.RespondAsync<IAccountCredentialsMessage>(new
                {
                    UserId = userCredentials.UserId,
                    Token = userCredentials.Token,
                    Name = userCredentials.Name,
                    Email = userCredentials.Email
                });
            }
            else
            {
                await context.RespondAsync<IRegisterNewAccountRejection>(new
                {
                    Value = "Account register/login fail!"
                });
            }
        }
    }
}
