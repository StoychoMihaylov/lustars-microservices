namespace WebGateway.Services.Interfaces
{
    using System.Threading.Tasks;
    using WebGateway.Models.ViewModels;
    using WebGateway.Models.BidingModels.Account;

    public interface IAccountService
    {
        Task<AccountCredentialsViewModel> CallAuthAPI_AccountRegister(RegisterUserBindingModel bm);
        Task<AccountCredentialsViewModel> CallAuthAPI_AccountLogin(LoginUserBindingModel bm);
        Task<bool> CallAuthAPI_AccountLogout(LogoutBindingModel bm);
        void CallAuthAPI_DeleteAccount(AccountCredentialsViewModel userProfile);
    }
}
