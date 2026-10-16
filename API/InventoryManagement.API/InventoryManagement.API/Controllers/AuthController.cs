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
            return Ok(result);
        }

    }
}
