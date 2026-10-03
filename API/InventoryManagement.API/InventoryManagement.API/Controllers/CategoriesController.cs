using InventoryManagement.Application.Commands.CategoryCommands;
using InventoryManagement.Application.DTOs.CommandDTOs.CategoryCommandDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.CategoryQueryDTOs;
using InventoryManagement.Application.Queries.CategoryQueries;
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
            return CreatedAtAction(nameof(GetCategoryById), new { categoryId = result.Id }, result);
        }
        //GET : {apiBaseUrl}/api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllCategoriesResponseDTO>>> GetAllCategories()
        {
            var result = await mediator.Send(new GetAllCategoriesQuery());
            return Ok(result);
        }
        //GET : {apiBaseUrl}/api/categories/{categoryId}
        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId)
        {
            var result = await mediator.Send(new GetCategoryByIdQuery(categoryId));
            return Ok(result);
        }
        //PUT : {apiBaseUrl}/api/categories/{categoryId}
        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId, [FromBody] UpdateCategoryRequestDTO newCategoryData)
        {
            var result = await mediator.Send(new UpdateCategoryCommand(categoryId, newCategoryData));
            return Ok(result);
        }
        //DELETE : {apiBaseUrl}/api/categories/{categoryId}
        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            var result = await mediator.Send(new DeleteCategoryCommand(categoryId));
            return Ok($"دسته بندی با شناسه {categoryId} حذف گردید");
        }
    }
}
