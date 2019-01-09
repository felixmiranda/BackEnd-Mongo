using System.Threading.Tasks;
using Posts.Entities.Models;

namespace Posts.DataAccess
{
    public interface IUserDataContext
    {
        Task<User> CreateUser(User user);
    }
}