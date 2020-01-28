namespace AuthAPI.Services.Interfaces
{
    using AuthAPI.Models.ViewModels;
    using AuthAPI.Models.BidingModels;

    public interface IAccountService
    {
        bool CheckIfUserExist(RegisterUserBindingModel bm);
        AccountCredentialsViewModel CreateNewUserAccount(RegisterUserBindingModel bm);
        AccountCredentialsViewModel LoginUser(LoginUserBindingModel bm);
        void DeleteUserToken(LogoutBindingModel bm);
        UserCredentials CheckIfTokenIsValidAndReturnUserCredentials(Token token);
    }
}
