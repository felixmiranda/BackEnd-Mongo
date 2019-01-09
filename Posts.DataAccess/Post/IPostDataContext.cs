using System.Collections.Generic;
using System.Threading.Tasks;
using Posts.Entities.Models;

namespace Posts.DataAccess
{
    public interface IPostDataContext
    {
        Task<List<Post>> GetListaPosts();
        Task<Post> GetById(string id);
        Task EliminarByID(string id);
        Task<Post> CrearPost(Post post);
        Task<Post> ActualizarPost(Post post);  
    }
}