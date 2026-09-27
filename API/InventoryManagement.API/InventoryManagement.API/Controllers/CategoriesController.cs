using InventoryManagement.Application.Commands.CategoryCommands;
using InventoryManagement.Application.DTOs.CommandDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.GetAllCategories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        //POST : {apiBaseUrl}/api/categories/addcategory
        [HttpPost("AddCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDTO request)
        {
            var result = await mediator.Send(new CreateCategoryCommand(request));
            return Ok(result);
        }
        //GET : {apiBaseUrl}/api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllCategoriesResponseDTO>>> GetAllCategories()
        {

        }
    }
}
