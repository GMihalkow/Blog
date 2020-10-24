using Blog.Web.Models.Account;
using System.Threading.Tasks;

namespace Blog.Web.Services.Accounts.Contracts
{
    public interface IAccountService
    {
        Task<bool> Login(LoginInputModel loginInputModel);

        Task Logout();
    }
}