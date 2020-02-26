namespace WebGateway.Services.Interfaces
{
    using System.Threading.Tasks;

    using WebGateway.Models.ViewModels;
    using WebGateway.Models.BidingModels.Account;
    using WebGateway.Models.DTOs;

    public interface IAccountService
    {
        Task<AccountCredentialsViewModel> CallAuthAPIAccountRegister(RegisterUserBindingModel bm);
        Task<AccountCredentialsViewModel> CallAuthAPIAccountLogin(LoginUserBindingModel bm);
        Task<bool> CallAuthAPIAccountLogout(LogoutBindingModel bm);
        void CallAuthAPIDeleteAccount(AccountCredentialsViewModel userProfile);
    }
}
