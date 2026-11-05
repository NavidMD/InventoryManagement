using InventoryManagement.Application.Commands.ReceiptCommands;
using InventoryManagement.Application.DTOs.CommandDTOs.ReceiptCommandDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.ReceiptDTOs;
using InventoryManagement.Application.Queries.ReceiptQueries;
using InventoryManagement.Core.Models;
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
            return CreatedAtAction(nameof(GetReceiptById), new { receiptId = result.Id }, result);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllReceiptsResponseDTO>>> GetAllReceipts()
        {
            var result = await mediator.Send(new GetAllReceiptsQuery());
            return Ok(result);
        }
        [HttpGet("{receiptId:guid}")]
        public async Task<IActionResult> GetReceiptById([FromRoute] Guid receiptId)
        {
            var result = await mediator.Send(new GetReceiptByIdQuery(receiptId));
            return Ok(result);
        }
        [HttpGet("filter/byEmployee/{employeeId:guid}")]
        public async Task<ActionResult<IEnumerable<GetReceiptsByDepartmentIdResponseDTO>>> GetReceiptByEmployeeId([FromRoute] Guid employeeId)
        {
            var result = await mediator.Send(new GetReceiptsByEmployeeIdQuery(employeeId));
            return Ok(result);
        }
        [HttpGet("filter/byDepartment/{departmentId:guid}")]
        public async Task<ActionResult<IEnumerable<GetReceiptsByDepartmentIdResponseDTO>>> GetReceiptsByDepartmentId([FromRoute] Guid departmentId)
        {
            var result = await mediator.Send(new GetReceiptsByDepartmentIdQuery(departmentId));
            return Ok(result);
        }
        [HttpGet("filter/byProduct/{productId:guid}")]
        public async Task<ActionResult<IEnumerable<GetReceiptsByProductIdResponseDTO>>> GetReceiptsByProductId([FromRoute] Guid productId)
        {
            var result = await mediator.Send(new GetReceiptsByProductIdQuery(productId));
            return Ok(result);
        }
        [HttpGet("filter/advanced")]
        public async Task<ActionResult<IEnumerable<ReceiptReadModel>>> GetReceiptsByAdvancedFilter([FromQuery] GetReceiptsByAdvancedFilterRequestDTO filters)
        {
            var result = await mediator.Send(new GetReceiptsByAdvancedFilterQuery(filters));
            return Ok(result);
        }
    }
}
