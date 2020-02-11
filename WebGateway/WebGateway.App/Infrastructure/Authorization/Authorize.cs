namespace WebGateway.App.Infrastructure.Authorization
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class Authorize : Attribute, IAuthorizationFilter
    {
        private AuthService authService = new AuthService();

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the request contains "Authorization" header
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authToken))
            {
                var token = authService.ExtraxtToken(authToken);

                // TO DO: 
                // check if token is cached
                //      if yes continue 
                //      if not check in AuthAPI
                //          if token is not in AuthAPI return Unauthorized!
                // end

                var storedToken = authService.CheckIfTokenExistInAuthAPIService(token);

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
    }
}
