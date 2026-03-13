using Hospital.Application.DTOs.Authentication;
using Hospital.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            try
            {
                var token = await _authService.Login(model);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            try
            {
                await _authService.Register(model);
                return Ok("Signed in Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
