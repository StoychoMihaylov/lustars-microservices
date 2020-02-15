namespace WebGateway.App.Infrastructure.Authorization
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Caching.Memory;

    public class Authorize : Attribute, IAuthorizationFilter
    {
        private readonly AuthAttributeService authService;
        private readonly MemoryCache cache;

        public Authorize()
        {
            this.authService = new AuthAttributeService();
            this.cache = new MemoryCache(new MemoryCacheOptions());
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the request contains "Authorization" header
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authToken))
            {
                var token = authService.ExtraxtToken(authToken);
                var isUserAuthorized = CheckIfToeknIsCachedIfNotCheckAuthAPIAndCacheIt(token);
                
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

        private bool CheckIfToeknIsCachedIfNotCheckAuthAPIAndCacheIt(string token)
        {
            MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
            cacheExpirationOptions.SlidingExpiration = TimeSpan.FromMinutes(30);
            cacheExpirationOptions.Priority = CacheItemPriority.Normal;

            string cachedToken = string.Empty;
            if (!cache.TryGetValue(token, out cachedToken))
            {
                var IsTokenExistingInAuthAPI = authService.CheckIfTokenExistInAuthAPIService(token);
                if (IsTokenExistingInAuthAPI)
                {
                    cache.Set<string>(token, token, cacheExpirationOptions);

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
