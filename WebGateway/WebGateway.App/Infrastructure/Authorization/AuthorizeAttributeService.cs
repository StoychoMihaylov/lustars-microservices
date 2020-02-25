namespace WebGateway.App.Infrastructure.Authorization
{
    using System;
    using System.Net;
    using System.Text;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Net.Http;
    using Microsoft.Extensions.Primitives;

    using WebGateway.Models.DTOs;
    using WebGateway.Services.Services;
    using WebGateway.Services.Endpoints;

    public class AuthorizeAttributeService : Service, IAuthorizeAttributeService
    {
        public AuthorizeAttributeService(HttpClient httpClient)
           : base(httpClient) { }

        public UserCredentials CheckIfTokenExistInAuthAPIService(string stringToken)
        {
            var stringContent = SetStringContent(stringToken);
            var response = this.HttpClient
                .PostAsync(AuthAPIService.Endpoint + "account/authorized", stringContent).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var userCredentials = JsonConvert
                    .DeserializeObject<UserCredentials>(response.Content.ReadAsStringAsync().Result);

                return userCredentials;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }

            return null;
        }

        public string ExtraxtToken(StringValues authToken)
        {
            string brearer = authToken.First();
            return brearer.Split(' ')[1];
        }

        public void SetGlobalCurrentUser(Guid userId, string token)
        {
            IdentityManager.SetCurrentUser(userId, token);
        }

        private StringContent SetStringContent(string stringToken)
        {
            var token = new Token(stringToken);
            var dataJSON = JsonConvert.SerializeObject(token);
            return new StringContent(dataJSON, Encoding.UTF8, "application/json");
        }
    }
}
