namespace WebGateway.App.Utilities
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Text;
    using System.Net.Http;
    using Newtonsoft.Json;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using Microsoft.AspNetCore.Mvc.Filters;

    using WebGateway.Models.DTOs;
    using WebGateway.Services.Endpoints;

    public class Authorize : Attribute, IAuthorizationFilter
    {
        private static HttpClient httpClient = new HttpClient();

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authToken)) // if counteins Auth header
            {
                var token = ExtraxtToken(authToken);       
                var storedToken = CheckIfTokenExistInAuthAPIService(new Token() { Value = token });

                if (storedToken == false)
                {
                    context.Result = new ContentResult { StatusCode = 401, Content = "User Unauthorized!" };
                }
            }
            else
            {
                context.Result = new ContentResult { StatusCode = 401, Content = "User Unauthorized!" };
            }
        }

        private string ExtraxtToken(StringValues authToken)
        {
            string brearer = authToken.First();
            return brearer.Split(' ')[1];
        }

        private bool CheckIfTokenExistInAuthAPIService(Token token)
        {
            var response = new HttpResponseMessage();
            var dataJSON = JsonConvert.SerializeObject(token);
            var stringContent = new StringContent(dataJSON, Encoding.UTF8, "application/json");

            response = httpClient.PostAsync(AuthAPIService.Endpoint + "account/authorized", stringContent).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var userCredentials = JsonConvert.DeserializeObject<UserCredentials>(response.Content.ReadAsStringAsync().Result);

                SetGlobalCurrentUser(userCredentials.UserId, userCredentials.Token); // Set Global User

                return true;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        private void SetGlobalCurrentUser(Guid userId, string token)
        {
            Identity.SetCurrentUserId(userId);
            Identity.SetCurrentUserToken(token);
        }
    }
}
