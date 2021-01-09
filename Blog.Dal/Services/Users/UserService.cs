using Blog.Dal.Models.User;
using Blog.Data;
using Blog.Web.Services.Users.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Dal.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BlogDbContext _dbContext;

        public UserService(BlogDbContext dbContext)
            => this._dbContext = dbContext;

        public async Task<IEnumerable<UserDropdownListModel>> GetAllForDropdown()
        {
            return await this._dbContext
                .Users
                .Select(u => new UserDropdownListModel
                {
                    Id = u.Id,
                    Name = u.UserName
                })
                .ToListAsync();
        }

        public async Task<string> GetUserIdByName(string name)
        {
            var user = await this._dbContext.Users.FirstOrDefaultAsync(u => u.UserName == name);

            if (user == null)
                throw new InvalidOperationException("No user found with this username.");

            return user.Id;
        }
    }
}