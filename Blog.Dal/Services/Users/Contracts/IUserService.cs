using System.Threading.Tasks;

namespace Blog.Web.Services.Users.Contracts
{
    public interface IUserService
    {
        Task<string> GetUserIdByName(string name);
    }
}