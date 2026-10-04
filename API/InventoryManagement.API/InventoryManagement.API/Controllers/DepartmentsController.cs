using InventoryManagement.Application.Commands.DepartmentCommands;
using InventoryManagement.Application.DTOs.CommandDTOs.DepartmentCommandDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.DepartmentQueryDTOs;
using InventoryManagement.Application.Queries.DepartmentQueries;
using InventoryManagement.Core.Entities;



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
        [HttpPost("createdepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequestDTO request)
        {
            var result = await mediator.Send(new CreateDepartmentCommand(request));
            return CreatedAtAction(nameof(GetDepartmentById),new { departmentId = result.Id }, result);
        }
        //GET : {apiBaseUrl}/api/departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllDepartmentsResponseDTO>>> GetAllDepartments()
        {
            var result = await mediator.Send(new GetAllDepartmentsQuery());
            return Ok(result);
        }
        //GET : {apiBaseUrl}/api/departments/{departmentId}
        //Route Constraint ==> this route is accessible only if departmentId is convertable to Guid
        [HttpGet("{departmentId:guid}")]
        public async Task<IActionResult> GetDepartmentById([FromRoute] Guid departmentId)
        {
            var result = await mediator.Send(new GetDepartmentByIdQuery(departmentId));
            return Ok(result);
        }
        //PUT : {apiBaseUrl}/api/departments/{departmentId}
        [HttpPut("{departmentId:guid}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid departmentId, [FromBody] UpdateDepartmentRequestDTO newDepartmentData)
        {
            var result = await mediator.Send(new UpdateDepartmentCommand(departmentId, newDepartmentData));
            return Ok(result);
        }
        //DELETE : {apiBaseUrl}/api/departments/{departmentId}
        [HttpDelete("{departmentId:guid}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] Guid departmentId)
        {
            var result = await mediator.Send(new DeleteDepartmentCommand(departmentId));
            return Ok($"دپارتمان با شناسه {departmentId} حذف گردید");
        }

    }
}
