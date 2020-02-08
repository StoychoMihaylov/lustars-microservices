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

    public class AccountService : IAccountService
    {
        private readonly HttpClient httpClient;

        public AccountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private StringContent SerializeObjectToStringContent(dynamic bm)
        {
            var dataJSON = JsonConvert.SerializeObject(bm);
            var stringContent = new StringContent(dataJSON, Encoding.UTF8, "application/json");

            return stringContent;
        }

        public async Task<AccountCredentialsViewModel> CallAuthAPIAccountLogin(LoginUserBindingModel bm)
        {
            var response = new HttpResponseMessage();
            var stringContent = SerializeObjectToStringContent(bm);

            try
            {
                response = await httpClient.PostAsync(AuthAPIService.Endpoint + "account/login", stringContent);
            }
            catch (Exception ex)
            {
                throw new Exception("AuthAPI service does not responding: " + ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<AccountCredentialsViewModel>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorMessage = response.Content.ReadAsStringAsync().Result;

                throw new Exception(errorMessage);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CallAuthAPIAccountLogout(LogoutBindingModel bm)
        {
            var response = new HttpResponseMessage();
            var stringContent = SerializeObjectToStringContent(bm);

            try
            {
                response = await httpClient.PostAsync(AuthAPIService.Endpoint + "account/logout", stringContent);
            }
            catch (Exception ex)
            {
                throw new Exception("AuthAPI service does not responding: " + ex);
            }

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
            var response = new HttpResponseMessage();
            var stringContent = SerializeObjectToStringContent(bm);

            try
            {
                response = await httpClient.PostAsync(AuthAPIService.Endpoint + "account/register", stringContent);
            }
            catch (Exception ex)
            {
                throw new Exception("AuthAPI service does not responding: " + ex);
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<AccountCredentialsViewModel>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorMessage = response.Content.ReadAsStringAsync().Result;

                throw new Exception(errorMessage);
            }
            else
            {
                return null;
            }
        }
    }
}
