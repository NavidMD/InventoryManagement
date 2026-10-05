using InventoryManagement.Application.DTOs.CommandDTOs.CategoryCommandDTOs;
using InventoryManagement.Application.DTOs.CommandDTOs.EmployeeCommandDTOs;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.EmployeeCommands
{
    public record CreateEmployeeCommand(CreateEmployeeRequestDTO newEmployee) : IRequest<CreateEmployeeResponseDTO>;
    public class CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository) : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponseDTO>
    {
        public async Task<CreateEmployeeResponseDTO> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                FirstName = request.newEmployee.FirstName,
                LastName = request.newEmployee.LastName,
                PersonnelCode = request.newEmployee.PersonnelCode,
                DepartmentId = request.newEmployee.DepartmentId
            };
            var response = await employeeRepository.CreateAsync(employee);
            return new CreateEmployeeResponseDTO()
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                PersonnelCode = response.PersonnelCode,
                DepartmentId = response.DepartmentId,
            };
        }
    }
}
