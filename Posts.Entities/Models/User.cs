using MongoDB.Bson.Serialization.Attributes;

namespace Posts.Entities.Models
{
    public class User
    {
         [BsonId]
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}