namespace WebGateway.Services.Services
{
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using WebGateway.Models.ViewModels;
    using WebGateway.Services.Endpoints;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.Account;

    public class AccountService : IAccountService
    {
        private readonly HttpClient httpClient;
        private readonly AuthAPIService authAPIService;

        public AccountService(HttpClient httpClient, AuthAPIService authAPIService)
        {
            this.httpClient = httpClient;
            this.authAPIService = authAPIService;
        }

        public async Task<AccountCredentialsViewModel> CallAuthAPIAccountLogin(LoginUserBindingModel bm)
        {
            var response = await httpClient.GetStringAsync(authAPIService.Endpoint + "account/login");
            return JsonConvert.DeserializeObject<AccountCredentialsViewModel>(response);
        }

        public async void CallAuthAPIAccountLogout(LogoutBindingModel bm)
        {
            await httpClient.GetStringAsync(authAPIService.Endpoint + "account/logout");
        }

        public async Task<AccountCredentialsViewModel> CallAuthAPIAccountRegister(RegisterUserBindingModel bm)
        {
            var response = await httpClient.GetStringAsync(authAPIService.Endpoint + "account/register");
            return JsonConvert.DeserializeObject<AccountCredentialsViewModel>(response);
        }
    }
}
