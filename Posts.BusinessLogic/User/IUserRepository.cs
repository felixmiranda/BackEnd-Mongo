using System.Threading.Tasks;
using Posts.Entities.Models;

namespace Posts.BusinessLogic
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> Authenticate(string username, string password);
         Task<User>  GetUserById(string id);
    }
}