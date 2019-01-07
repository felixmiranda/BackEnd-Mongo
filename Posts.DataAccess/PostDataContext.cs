using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Posts.Entities.Models;

namespace Posts.DataAccess
{
   
    public class PostDataContext : IPostDataContext
    {
        private readonly IMongoDatabase _database = null; 
        private readonly IMongoCollection<Post> _collection = null; 
        public PostDataContext(IOptions<DBSettings> dbsettings)
        {
            var mongoClient = new MongoClient(dbsettings.Value.ConnectionString);
            if(mongoClient !=null){
                _database = mongoClient.GetDatabase(dbsettings.Value.Database) ;
                _collection = _database.GetCollection<Post>("posts") ;
            }
        }
        public async Task<Post> ActualizarPost(Post post)
        { 
            var filter = Builders<Post>.Filter.Eq("Id", post.id);

            var update = Builders<Post>.Update
                            .Set("Titulo", post.Titulo)
                            .Set("Descripcion", post.Descripcion)
                            .Set("Autor", post.Autor)
                            .Set("Categoria", post.Categoria);

            var options = new FindOneAndUpdateOptions<Post,Post>{
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };                
            return await _collection.FindOneAndUpdateAsync(filter, update, options);
            /*
            post.Titulo = "Titulo actualizado";
            return await Task.FromResult(post) ;
            */
        }
         

        public async Task<Post> CrearPost(Post post)
        {
            await _collection.InsertOneAsync(post);
            return await GetById(post.id);
            /*
            post.id = "abc";
            return await Task.FromResult(post) ; 
            */
        }



        public async Task<Post> GetById(string id)
        {
            try
            {
                var filter = Builders<Post>.Filter.Eq("id",id);
                return await _collection.Find(filter).FirstOrDefaultAsync();
                // return await Task.FromResult(new Post{ id = "123", Titulo = "sadfasd"}) ;
            }
            catch(Exception ex)
            {
                throw ex; 
            }

          
        }

        public async Task<List<Post>> GetListaPosts()
        {
            var result = await _collection.Find((x)=> true).ToListAsync();
            if (result == null) return null;
            if (result.Count == 0) return null; 

            return result; 
            /*
            return await Task.FromResult(
                new List<Post> { 
                    new Post{ id = "123", Titulo = "aaaaa"},
                    new Post{ id = "456", Titulo = "ddssdsd"}
             }); 
              */
        }

        public async Task  EliminarByID(string id)
        {
            var filter = Builders<Post>.Filter.Eq("Id", id);
            await  _collection.DeleteOneAsync(filter);
        }
    }
    
}