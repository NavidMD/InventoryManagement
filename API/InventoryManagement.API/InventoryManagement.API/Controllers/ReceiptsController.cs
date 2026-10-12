using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController(IMediator mediator) : ControllerBase
    {
        //[HttpPost("addreceipt")]
        //public async Task<IActionResult> AddReceipt([FromBody] CreateReceiptRequestDTO request)
        //{

        //}
    }
}
