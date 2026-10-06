using InventoryManagement.Application.Commands.EmployeeCommands;
using InventoryManagement.Application.DTOs.CommandDTOs.EmployeeCommandDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.EmployeeQueryDTOs;
using InventoryManagement.Application.Queries.EmployeeQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IMediator mediator) : ControllerBase
    {
        //POST : {apibaseurl}/api/employees/addemployee
        [HttpPost("addemployee")]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeRequestDTO requset)
        {
            var result = await mediator.Send(new CreateEmployeeCommand(requset));
            return CreatedAtAction(nameof(GetEmployeeById), new { employeeId = result.Id }, result);
        }
        //GET : {apibaseurl}/api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllEmployeesResponseDTO>>> GetAllEmployees()
        {
            var result = await mediator.Send(new GetAllEmployeesQuery());
            return Ok(result);
        }
        //GET : {apibaseurl}/api/employees/{employeeId}
        [HttpGet("{employeeId:guid}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid employeeId)
        {
            var result = await mediator.Send(new GetEmployeeByIdQuery(employeeId));
            return Ok(result);
        }
        //PUT : {apibaseurl}/api/employees/{employeeId}
        [HttpPut("{employeeId:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid employeeId, [FromBody] UpdateEmployeeRequestDTO newEmployeeData)
        {
            var result = await mediator.Send(new UpdateEmployeeCommand(employeeId, newEmployeeData));
            return Ok(result);
        }
    }
}
