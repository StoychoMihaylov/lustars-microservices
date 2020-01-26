namespace WebGateway.Services.Interfaces
{
    using System.Threading.Tasks;
    using WebGateway.Models.ViewModels;
    using WebGateway.Models.BidingModels.Account;

    public interface IAccountService
    {
        Task<AccountCredentialsViewModel> CallAuthAPIAccountRegister(RegisterUserBindingModel bm);
        Task<AccountCredentialsViewModel> CallAuthAPIAccountLogin(LoginUserBindingModel bm);
        void CallAuthAPIAccountLogout(LogoutBindingModel bm);
    }
}
