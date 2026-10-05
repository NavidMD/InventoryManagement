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
            //return CreatedAtAction(nameof(GetEmployeeById), new { employeeId = result.Id }, result);
            return Ok(result);
        }
        //GET : {apibaseurl}/api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllEmployeesResponseDTO>>> GetAllEmployees()
        {
            var result = await mediator.Send(new GetAllEmployeesQuery());
            return Ok(result);
        }

    }
}
