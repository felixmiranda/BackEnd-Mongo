using System.Collections.Generic;
using System.Threading.Tasks;
using Posts.Entities.Models;

namespace Posts.BusinessLogic
{
    public interface IPostRepository
    {
        Task<List<Post>> GetListaPosts();
        Task<Post> GetById(string id);
        Task EliminarById(string id);
        Task<Post> CrearPost(Post post);
        Task<Post> ActualizarPost(Post post);
    }
}