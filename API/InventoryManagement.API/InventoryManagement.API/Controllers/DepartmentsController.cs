using InventoryManagement.Application.Commands.DepartmentCommands;
using InventoryManagement.Application.DTOs.CommandDTOs.DepartmentCommandDTOs;
//using InventoryManagement.Application.DTOs.QueryDTOs.DepartmentQueryDTOs;
//using InventoryManagement.Application.Queries.DepartmentQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IMediator mediator) : ControllerBase
    {
        //POST : {apiBaseUrl}/api/departments/createdepartment
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequestDTO request)
        {
            var result = await mediator.Send(new CreateDepartmentCommand(request));
            //return CreatedAtAction(nameof(GetDepartmentById),new { departmentId = result.Id }, result);
            return Created($"/api/departments/${result.Id}", result);
        }

        private object GetDepartmentById()
        {
            throw new NotImplementedException();
        }
    }
}
