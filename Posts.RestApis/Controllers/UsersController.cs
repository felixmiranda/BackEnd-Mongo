using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Posts.BusinessLogic;
using Posts.Entities.Models;

namespace Posts.RestApis.Controllers
{

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

    [HttpGet]
    public IActionResult Get()
    {
      try
      {
        return Ok();
      }
      catch (Exception)
      {
        _logger.LogError("Failed to execute GET");
        return BadRequest();
      }
    }

    [HttpPost]
    public IActionResult Post([FromBody] UserDTO userDTO)
    {
      try
      {
        var user = _mapper.Map<User>(userDTO);
        var userCreated = _repository.CreateUser(user);
        return CreatedAtAction(nameof(Get), new { id = userCreated.Id, userCreated});
      }
      catch (Exception)
      {
        _logger.LogError("Failed to execute POST");
        return BadRequest();
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