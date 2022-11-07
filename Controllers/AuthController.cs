using Microsoft.AspNetCore.Mvc;
using TasksWithRepositoryPattern.Models;
using TasksWithRepositoryPattern.Data;
using TasksWithRepositoryPattern.Dtos;
using TasksWithRepositoryPattern.Exceptions;
using TasksWithRepositoryPattern.Services;

namespace TasksWithRepositoryPattern.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private IUserService _service;
    public AuthController(IUserService service)
    {
        _service = service;
    }
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<UserDto>> Register(UserDto request)
    {
        var user = await _service.Register(request);
        
        return Ok(user);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto request)
    {
        try
        {
            return Ok(await _service.Login(request));
        }
        catch (AuthException e)
        {
            return NotFound(e.Errors);
        }
    }
    

}