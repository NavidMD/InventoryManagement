using InventoryManagement.Application.DTOs.CommandDTOs.CategoryCommandDTOs;
using InventoryManagement.Application.DTOs.CommandDTOs.EmployeeCommandDTOs;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Core.Interfaces.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = InventoryManagement.Application.Exceptions.ApplicationException;

namespace InventoryManagement.Application.Commands.EmployeeCommands
{
    public record CreateEmployeeCommand(CreateEmployeeRequestDTO newEmployee) : IRequest<CreateEmployeeResponseDTO>;
    public class CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUserService userService) : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponseDTO>
    {
        public async Task<CreateEmployeeResponseDTO> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                FirstName = request.newEmployee.FirstName,
                LastName = request.newEmployee.LastName,
                PersonnelCode = request.newEmployee.PersonnelCode,
                PhoneNumber = request.newEmployee.PhoneNumber,
                DepartmentId = request.newEmployee.DepartmentId,
                EmailAddress = request.newEmployee.EmailAddress,
            };
            var response = await employeeRepository.CreateAsync(employee);
            try
            {
                var userId = await userService.CreateUserAsync(response);
                return new CreateEmployeeResponseDTO()
                {
                    Id = response.Id,
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    PersonnelCode = response.PersonnelCode,
                    PhoneNumber = response.PhoneNumber,
                    DepartmentId = response.DepartmentId,
                    EmailAddress = response.EmailAddress,
                    UserId = userId
                };
            }
            catch (ApplicationException)
            {
                await employeeRepository.DeleteAsync(response.Id);
                throw;
            }
        }
    }
}
