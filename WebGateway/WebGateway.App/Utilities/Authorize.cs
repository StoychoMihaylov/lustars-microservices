namespace WebGateway.App.Utilities
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Text;
    using System.Net.Http;
    using Newtonsoft.Json;
    using WebGateway.Models.DTOs;
    using Microsoft.AspNetCore.Mvc;
    using WebGateway.Services.Endpoints;
    using Microsoft.Extensions.Primitives;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class Authorize : Attribute, IAuthorizationFilter
    {
        private static HttpClient httpClient = new HttpClient();

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authToken))
            {
                string brearer = authToken.First();
                string token = brearer.Split(' ')[1];
                var storedToken = false;

                storedToken = CheckIfTokenExistInAuthAPIService(new Token() { Value = token });

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

        private bool CheckIfTokenExistInAuthAPIService(Token token)
        {
            var response = new HttpResponseMessage();
            var dataJSON = JsonConvert.SerializeObject(token);
            var stringContent = new StringContent(dataJSON, UnicodeEncoding.UTF8, "application/json");

            try
            {
                response = httpClient.PostAsync(AuthAPIService.Endpoint + "account/authorized", stringContent).Result;
            }
            catch
            {
                return false;
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var userCredentials = JsonConvert.DeserializeObject<UserCredentials>(response.Content.ReadAsStringAsync().Result);

                SetGlobalCurrentUser(userCredentials.UserId);

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

        private void SetGlobalCurrentUser(Guid userId)
        {
            Identity.Id = userId;
        }
    }
}
