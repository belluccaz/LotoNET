using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LotoNET.Application.Interfaces;
using LotoNET.Application.DTOs.Auth;

namespace LotoNET.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("google")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] GoogleLoginDto dto)
        {
            var result = await _authService.LoginWithGoogleAsync(dto);

            if (result is null)
                return Unauthorized("Token inválido ou falha na autenticação.");

            return Ok(result);
        }
    }
}
