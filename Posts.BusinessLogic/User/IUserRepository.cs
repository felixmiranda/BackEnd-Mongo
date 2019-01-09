using System.Threading.Tasks;
using Posts.Entities.Models;

namespace Posts.BusinessLogic
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
    }
}