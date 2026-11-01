using InventoryManagement.Application.Commands.ReceiptCommands;
using InventoryManagement.Application.DTOs.CommandDTOs.ReceiptCommandDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.ReceiptDTOs;
using InventoryManagement.Application.Queries.ReceiptQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController(IMediator mediator) : ControllerBase
    {
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitReceipt([FromBody] CreateReceiptRequestDTO request)
        {
            var result = await mediator.Send(new CreateReceiptCommand(request));
            return Ok(result);
            //return CreatedAtAction(nameof(GetReceiptById), new { receiptId = result.Id }, result);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllReceiptsResponseDTO>>> GetAllReceipts()
        {
            var result = await mediator.Send(new GetAllReceiptsQuery());
            return Ok(result);
        }
    }
}
