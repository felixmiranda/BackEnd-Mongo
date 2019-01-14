using System.Threading.Tasks;
using Posts.Entities.Models;

namespace Posts.DataAccess
{
    public interface IUserDataContext
    {
        Task<User> CreateUser(User user);
        Task<User>  GetuserByUsername(string username);
        Task<User> GetUserById(string id);
    }
}