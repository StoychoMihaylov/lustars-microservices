namespace WebGateway.App.Infrastructure.Authorization
{
    using System;
    using Microsoft.Extensions.Primitives;

    using WebGateway.Models.DTOs;

    public interface IAuthorizeAttributeService
    {
        void SetGlobalCurrentUser(Guid userId, string token);
        UserCredentials CheckIfTokenExistInAuthAPIService(string stringToken);
        string ExtraxtToken(StringValues authToken);
    }
}
