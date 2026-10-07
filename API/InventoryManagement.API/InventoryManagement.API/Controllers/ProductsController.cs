using InventoryManagement.Application.Commands.EmployeeCommands;
using InventoryManagement.Application.Commands.ProductCommands;
using InventoryManagement.Application.DTOs.CommandDTOs.EmployeeCommandDTOs;
using InventoryManagement.Application.DTOs.CommandDTOs.ProductCommandDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.EmployeeQueryDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.ProductQueryDTOs;
using InventoryManagement.Application.Queries.EmployeeQueries;
using InventoryManagement.Application.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        //POST : {apibaseurl}/api/products/addproduct
        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductRequestDTO requset)
        {
            var result = await mediator.Send(new CreateProductCommand(requset));
            return CreatedAtAction(nameof(GetProductById), new { productId = result.Id }, result);
        }
        //GET : {apibaseurl}/api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllProductsResponseDTO>>> GetAllProducts()
        {
            var result = await mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }
        //GET : {apibaseurl}/api/products/{productId}
        [HttpGet("{productId:guid}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid productId)
        {
            var result = await mediator.Send(new GetProductByIdQuery(productId));
            return Ok(result);
        }
    }
}
