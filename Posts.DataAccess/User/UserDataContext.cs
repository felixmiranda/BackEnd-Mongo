using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Posts.Entities.Models;

namespace Posts.DataAccess
{
    public class UserDataContext : IUserDataContext
    {
        private readonly IMongoDatabase _database = null; 
        private readonly IMongoCollection<User> _collection = null; 
        public UserDataContext(IOptions<DBSettings> dbsettings)
        {
            var mongoClient = new MongoClient(dbsettings.Value.ConnectionString);
            if(mongoClient !=null){
                _database = mongoClient.GetDatabase(dbsettings.Value.Database) ;
                _collection = _database.GetCollection<User>("users") ;
            }
        }
        public async Task<User> CreateUser(User user)
        {
             var filter = Builders<User>.Filter.Eq("Id", user.Id);

            var updateDefinition = Builders<User>.Update
                            .Set("Nombres", user.Nombres)
                            .Set("Apellidos", user.Apellidos)
                            .Set("Username", user.Username)
                            .Set("Password", user.Password);

            var options = new FindOneAndUpdateOptions<User,User>{
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };                
            return await  _collection.FindOneAndUpdateAsync(filter, updateDefinition, options);
            
        }

        public async Task<User> GetUserById(string id)
        {
             var filter = Builders<User>.Filter.Eq("Id",id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetuserByUsername(string username)
        {
            var filter = Builders<User>.Filter.Eq("Username",username);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}