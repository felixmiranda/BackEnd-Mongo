using System.Threading.Tasks;
using Posts.DataAccess;
using Posts.Entities.Models;

namespace Posts.BusinessLogic
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDataContext _context;
        public UserRepository(IUserDataContext context)
        {
            _context = context; 
        }

        public async Task<User> CreateUser(User user)
        {
             return await _context.CreateUser(user);
        }


    }
}