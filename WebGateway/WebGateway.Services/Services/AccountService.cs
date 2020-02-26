namespace WebGateway.Services.Services
{
    using System;
    using System.Net;
    using System.Text;
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    using WebGateway.Models.ViewModels;
    using WebGateway.Services.Endpoints;
    using WebGateway.Services.Interfaces;
    using WebGateway.Models.BidingModels.Account;
    using WebGateway.Models.DTOs;

    public class AccountService : Service, IAccountService
    {
        public AccountService(HttpClient httpClient)
            : base(httpClient) { }

        private StringContent SerializeObjectToStringContent(dynamic bm)
        {
            var dataJSON = JsonConvert.SerializeObject(bm);
            var stringContent = new StringContent(dataJSON, Encoding.UTF8, "application/json");

            return stringContent;
        }

        public async Task<AccountCredentialsViewModel> CallAuthAPIAccountLogin(LoginUserBindingModel bm)
        {
            var stringContent = SerializeObjectToStringContent(bm);
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

        public async Task<bool> CallAuthAPIAccountLogout(LogoutBindingModel bm)
        {
            var stringContent = SerializeObjectToStringContent(bm);
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

        public async Task<AccountCredentialsViewModel> CallAuthAPIAccountRegister(RegisterUserBindingModel bm)
        { 
            var stringContent = SerializeObjectToStringContent(bm);
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

        public async void CallAuthAPIDeleteAccount(AccountCredentialsViewModel userProfile)
        {
            var stringContent = SerializeObjectToStringContent(userProfile);
            var response = await this.HttpClient.PostAsync(AuthAPIService.Endpoint + "account/delete", stringContent);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception($"NotFound: failed trying to delete account with id:{userProfile.UserId}");
            }
            else if (response.StatusCode == HttpStatusCode.NotImplemented)
            {
                throw new Exception($"NotImplemented: failed trying to delete account with id:{userProfile.UserId}");
            }
        }
    }
}
