namespace WebGateway.Services.Interfaces
{
    using System;
    using WebGateway.Models.DTOs;
    using Microsoft.Extensions.Primitives;

    public interface IAuthorizeAttributeService
    {
        void SetGlobalCurrentUser(Guid userId, string token);
        UserCredentials CheckIfTokenExistInAuthAPIService(string stringToken);
        string ExtraxtToken(StringValues authToken);
    }
}
