using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using Posts.DataAccess;
using Posts.Entities.Models;

namespace Posts.BusinessLogic
{
    public class PostRepository : IPostRepository
    {
        private readonly IPostDataContext _context;
        
        public PostRepository(IPostDataContext context)
        {
            _context = context;
        }

        public async Task<Post> ActualizarPost(Post post)
        {
            return await _context.ActualizarPost(post);
        }

        public async Task<Post> CrearPost(Post post)
        {
            post.id = ObjectId.GenerateNewId().ToString();
            return await _context.CrearPost(post);
        }

        public async Task EliminarById(string id)
        {
            await _context.EliminarByID(id);
        }

        public async Task<Post> GetById(string id)
        {
            return await _context.GetById(id);
        }

        public async Task<List<Post>> GetListaPosts()
        {
            return await _context.GetListaPosts();
        }
    }

}
