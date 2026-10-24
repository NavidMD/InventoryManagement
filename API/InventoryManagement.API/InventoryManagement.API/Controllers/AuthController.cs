using InventoryManagement.Application.Commands.AuthCommands;
using InventoryManagement.Application.DTOs.CommandDTOs.AuthCommandDTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        //POST : {apibaseurl}/api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var result = await mediator.Send(new LoginUserCommand(request));
            return Ok(result);
        }
        //POST : {apibaseurl}/api/auth/verify-otp
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VertificationRequestDTO request)
        {
            var result = await mediator.Send(new VerifyOtpCommand(request));
            Response.Cookies.Append("refreshToken", result.RefreshToken.RawRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = new DateTimeOffset(DateTime.SpecifyKind(result.RefreshToken.ExpiresAt, DateTimeKind.Utc))
            });
            return Ok(result.AccessToken);
        }
        //POST : {apibaseurl}/api/auth/refresh
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var result = await mediator.Send(new RefreshTokenCommand());
            return Ok(result);
        }
    }
}
