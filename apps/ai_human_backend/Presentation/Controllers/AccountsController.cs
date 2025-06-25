
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ServiceAbstraction;
using Shared.DTO.Auth;
using System.IO;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
  
    public class AccountsController(IServiceManager _serviceManager) : ControllerBase
    {


        [HttpPost("Login")]//Post : /api/Accounts/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);
        }

        [HttpPost("Register")]//Post : /api/Accounts/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }

        [HttpGet(template: "CheckEmail")]// GET BaseUrl/api/Authentication/CheckEmail

        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var Result = await _serviceManager.AuthenticationService.CheckEmailAsync(Email);
            return Ok(value: Result);
        }

    }
    
}
