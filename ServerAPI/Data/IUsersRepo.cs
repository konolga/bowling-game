using System.Collections.Generic;
using System.Threading.Tasks;
using ServerAPI.Infrastructure;

namespace ServerAPI.Data
{
    public interface IUsersRepo
    {
        Task CreateUser(User user);
        Task<User> GetUserByUsername(string username);
        Task<bool> UpdateUser(User user);
        Task DeleteUser(string id);

    }
}