using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using Posts.DataAccess;
using Posts.Entities.Models;

namespace Posts.BusinessLogic
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDataContext _context;
        private readonly AppSettings _appSettings; 
        public UserRepository(IUserDataContext context,AppSettings appSettings)
        {
            _context = context; 
            _appSettings = appSettings; 
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;

            var user = await _context.GetuserByUsername(username);

            if(user == null) return null; 

            if(password != user.Password) return null;

            user.Token = GenerarToken(user);

            return user; 
        }

        private string GenerarToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TimeExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString; 
        }

        public async Task<User> CreateUser(User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
             return await _context.CreateUser(user);
        }

        public async Task<User> GetUserById(string id)
        {
             return await _context.GetUserById(id);
        }
    }
}