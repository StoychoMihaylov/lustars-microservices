namespace AuthAPI.Messaging.Consumers
{
    using MassTransit;
    using System.Threading.Tasks;
    using MessageExchangeContract;
    using AuthAPI.Models.ViewModels;
    using AuthAPI.Services.Interfaces;

    public class DeleteAccountConsumer : IConsumer<IDeleteAccountProfile>
    {
        private readonly IAccountService service;

        public DeleteAccountConsumer(IAccountService service)
        {
            this.service = service;
        }

        public async Task Consume(ConsumeContext<IDeleteAccountProfile> context)
        { 
            var message = context.Message;

            var bm = new AccountCredentialsViewModel()
            {
                UserId = message.Id
            };
            
            this.service.DeleteUser(bm);
        }
    }
}
