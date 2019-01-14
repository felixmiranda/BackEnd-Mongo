using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Posts.BusinessLogic;
using Posts.Entities.Models;

namespace Posts.RestApis.Controllers
{
  [Authorize]
  [Route("api/users")]
  public class UsersController : Controller
  {

    ILogger<UsersController> _logger;
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    public UsersController(ILogger<UsersController> logger, IUserRepository repository, IMapper mapper)
    {
      _logger = logger;
      _repository = repository;
      _mapper = mapper; 
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id )
    {
      try
      {
        var user = await _repository.GetUserById(id);

        return Ok(user);
        
      }
      catch (Exception)
      {
        _logger.LogError("Failed to execute GET");
        return BadRequest();
      }
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserDTO userDTO)
    {
      try
      {
        var user = _mapper.Map<User>(userDTO);
        var userCreated = await _repository.CreateUser(user);
        //return CreatedAtAction(nameof(Get), new { id = userCreated.Id, userCreated});
        return  Ok(userCreated);
      }
      catch (Exception)
      {
        _logger.LogError("Failed to execute POST");
        return BadRequest();
      }
    }
        
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserDTO userDTO)
    {
      try {
          var result = await _repository.Authenticate(userDTO.Username, userDTO.Password);

          return Ok(result);
      }
      catch(Exception ex)
      {
        throw ex;
      }
      
    }

    [HttpPut]
    public IActionResult Put([FromBody] UserDTO model)
    {
      try
      {
        return Ok();
      }
      catch (Exception)
      {
        _logger.LogError("Failed to execute PUT");
        return BadRequest();
      }
    }

    [HttpDelete]
    public IActionResult Delete(string id)
    {
      try
      {
        return Ok();
      }
      catch (Exception)
      {
        _logger.LogError("Failed to execute DELETE");
        return BadRequest();
      }
    }
  }
}