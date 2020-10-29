namespace AuthAPI.Services.Interfaces
{
    using System.Threading.Tasks;
    using AuthAPI.Models.ViewModels;
    using AuthAPI.Models.BidingModels;

    public interface IAccountService
    {
        bool CheckIfUserExist(RegisterUserBindingModel bm);
        Task<AccountCredentialsViewModel> CreateNewUserAccount(RegisterUserBindingModel bm);
        Task<AccountCredentialsViewModel> LoginUser(LoginUserBindingModel bm);
        void DeleteUserToken(LogoutBindingModel bm);
        UserCredentials CheckIfTokenIsValidAndReturnUserCredentials(Token token);
        bool DeleteUser(AccountCredentialsViewModel accountCredentialsVm);
    }
}
