using MongoDB.Bson.Serialization.Attributes;

namespace Posts.Entities.Models
{
    
    public class Post
    {
        [BsonId]
        public string id { get; set; } 
        public string Titulo { get; set; }
        public string Autor { get; set; }       
        public string Descripcion { get; set; } 
        public int Categoria { get; set; }  
    }
}