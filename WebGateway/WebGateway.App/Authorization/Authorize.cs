namespace WebGateway.App.Authorization
{
    using System;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Caching.Memory;

    using WebGateway.Services.Common;

    public class Authorize : Attribute, IAuthorizationFilter
    {
        private readonly MemoryCache cache;

        private readonly IAuthorizeAttributeService authService;

        public Authorize()
        {
            this.authService = new AuthorizeAttributeService(new HttpClient(), new StringContentSerializer());
            this.cache = new MemoryCache(new MemoryCacheOptions());
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the request contains "Authorization" header
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authToken))
            {
                var token = authService.ExtraxtToken(authToken);
                var isUserAuthorized = CheckIfTokenIsCachedIfNotCheckAuthAPIAndCacheIt(token);
                
                if (isUserAuthorized == false)
                {
                    context.Result = new ContentResult { StatusCode = 401, Content = "User Unauthorized!" };
                }
            }
            else
            {
                context.Result = new ContentResult { StatusCode = 401, Content = "User Unauthorized!" };
            }
        }

        private bool CheckIfTokenIsCachedIfNotCheckAuthAPIAndCacheIt(string token)
        {
            string cachedCredentials = string.Empty;
            if (!cache.TryGetValue(token, out cachedCredentials))
            {
                var userCredentials = authService.CheckIfTokenExistInAuthAPIService(token);
                if (userCredentials != null)
                {
                    this.authService.SetGlobalCurrentUser(userCredentials.UserId, userCredentials.Token); // Set Global User

                    var cacheExpirationOptions = SetCacheExpirationOptions();
                    cache.Set<string>(token, userCredentials.UserId.ToString() + "::" + userCredentials.Token, cacheExpirationOptions);

                    return true;
                }
                else
                {
                    return false;
                }
            }

            var credentialsArray = cachedCredentials.Split("::");
            this.authService.SetGlobalCurrentUser(new Guid(credentialsArray[0]), credentialsArray[1]); // Set Global User

            return true;
        }

        private MemoryCacheEntryOptions SetCacheExpirationOptions()
        {
            MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
            cacheExpirationOptions.SlidingExpiration = TimeSpan.FromMinutes(1);
            cacheExpirationOptions.Priority = CacheItemPriority.Normal;

            return cacheExpirationOptions;
        }
    }
}
