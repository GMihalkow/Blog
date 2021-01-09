using Blog.Dal.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Web.Services.Users.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDropdownListModel>> GetAllForDropdown();

        Task<string> GetUserIdByName(string name);
    }
}