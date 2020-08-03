namespace WebGateway.Services.Services
{
    using System.Net;
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using WebGateway.Services.Common;
    using WebGateway.Models.ViewModels;
    using WebGateway.Services.Endpoints;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.Account;

    public class AccountService : Service, IAccountService
    {
        public AccountService(HttpClient httpClient, StringContentSerializer stringContentSerializer)
            : base(httpClient, stringContentSerializer) { }

        public async Task<AccountCredentialsViewModel> CallAuthAPI_AccountLogin(LoginUserBindingModel bm)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(bm);
            var response = await this.HttpClient.PostAsync(AuthAPIService.Endpoint + "account/login", stringContent);
       
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var accountCredentialsVm = JsonConvert
                    .DeserializeObject<AccountCredentialsViewModel>(response.Content.ReadAsStringAsync().Result);

                return accountCredentialsVm;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CallAuthAPI_AccountLogout(LogoutBindingModel bm)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(bm);
            var response = await this.HttpClient.PostAsync(AuthAPIService.Endpoint + "account/logout", stringContent);
      
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<AccountCredentialsViewModel> CallAuthAPI_AccountRegister(RegisterUserBindingModel bm)
        { 
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(bm);
            var response = await this.HttpClient.PostAsync(AuthAPIService.Endpoint + "account/register", stringContent);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var accountCredentialsVm = JsonConvert
                    .DeserializeObject<AccountCredentialsViewModel>(response.Content.ReadAsStringAsync().Result);

                return accountCredentialsVm;
            }
            else
            {
                return null;
            }
        }

        public async void CallAuthAPI_DeleteAccount(AccountCredentialsViewModel userProfile)
        {
            var stringContent = this.StringContentSerializer.SerializeObjectToStringContent(userProfile);
            await this.HttpClient.PostAsync(AuthAPIService.Endpoint + "account/delete", stringContent);
        }
    }
}
