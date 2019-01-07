using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Posts.BusinessLogic;
using Posts.Entities.Models;

namespace Posts.RestApis.Controllers
{
    [Route("api/[controller]")]
    public class PostsController: Controller
    {
        private readonly IPostRepository _repository;
        public PostsController(IPostRepository repository)
        {
            _repository = repository;
        }
        //listar un   post
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var posts = await _repository.GetListaPosts();
            if (posts == null ) return NotFound();

            return Ok(posts);
        }
        //obtener un post
        //GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var posts =  await _repository.GetById(id);
            if (posts == null) return NotFound();

            return Ok(posts);
        }

        //crear un post
         // POST api/values
        [HttpPost]
        public  async Task<ActionResult>  Post([FromBody] Post post)
        {
            var postResult = await _repository.CrearPost(post);
            
            return CreatedAtAction(nameof(Get), new {id = postResult.id}, postResult);
        }
        // Actualizar un post
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Post post)
        {
            var postExist = await _repository.GetById(id);
            if(postExist == null) return NotFound();

            post.id = id; 
            var result = await _repository.ActualizarPost(post);
            return Ok(result);    
        
        }
        //eliminar un post
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _repository.EliminarById(id) ;
            return NoContent();
        }

    }
}