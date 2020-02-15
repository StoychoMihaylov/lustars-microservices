namespace WebGateway.App.Infrastructure.Authorization
{
    using System;
    using System.Net;
    using System.Text;
    using System.Linq;
    using System.Net.Http;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Primitives;

    using WebGateway.Models.DTOs;
    using WebGateway.Services.Endpoints;

    public class AuthAttributeService
    {
        private HttpClient httpClient;

        public AuthAttributeService()
        {
            this.httpClient = new HttpClient();
        }

        private void SetGlobalCurrentUser(Guid userId, string token)
        {
            IdentityManager.SetCurrentUser(userId, token);
        }

        private StringContent SetStringContent(string stringToken)
        {
            var token = new Token(stringToken);
            var dataJSON = JsonConvert.SerializeObject(token);
            return new StringContent(dataJSON, Encoding.UTF8, "application/json");
        }

        public bool CheckIfTokenExistInAuthAPIService(string stringToken)
        {
            var stringContent = SetStringContent(stringToken);
            var response = httpClient
                .PostAsync(AuthAPIService.Endpoint + "account/authorized", stringContent).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var userCredentials = JsonConvert
                    .DeserializeObject<UserCredentials>(response.Content.ReadAsStringAsync().Result);

                SetGlobalCurrentUser(userCredentials.UserId, userCredentials.Token); // Set Global User

                return true;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }
                     
            return false;      
        }

        public string ExtraxtToken(StringValues authToken)
        {
            string brearer = authToken.First();
            return brearer.Split(' ')[1];
        }  
    }
}
