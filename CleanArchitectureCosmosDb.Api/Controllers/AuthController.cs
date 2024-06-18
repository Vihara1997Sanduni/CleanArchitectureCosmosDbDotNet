using Microsoft.AspNetCore.Mvc;
using CleanArchitectureCosmosDb.Application.Services;
using System.Threading.Tasks;
using CleanArchitectureCosmosDb.Application.DTOs;

namespace CleanArchitectureCosmosDb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserDto userDto)
        {
            var authResponse = await _authService.Authenticate(userDto);
            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
    }
}
