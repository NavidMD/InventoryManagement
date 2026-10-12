using InventoryManagement.Application.Commands.DepartmentCommands;
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
    public record UpdateEmployeeCommand(Guid employeeId, UpdateEmployeeRequestDTO updatedEmployee) : IRequest<UpdateEmployeeResponseDTO>;
    public class UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository) : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeResponseDTO>
    {
        public async Task<UpdateEmployeeResponseDTO> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var newEmployee = new Employee()
            {
                Id = request.employeeId,
                FirstName = request.updatedEmployee.FirstName,
                LastName = request.updatedEmployee.LastName,
                PhoneNumber = request.updatedEmployee.PhoneNumber,
                PersonnelCode = request.updatedEmployee.PersonnelCode,
                EmailAddress = request.updatedEmployee.EmailAddress
            };
            var response = await employeeRepository.UpdateAsync(request.employeeId, newEmployee);
            if (response == null)
            {
                throw new KeyNotFoundException("کارمندی با این شناسه جهت ویرایش یافت نشد!");
            }
            return new UpdateEmployeeResponseDTO()
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                PersonnelCode = response.PersonnelCode,
                PhoneNumber = response.PhoneNumber,
                EmailAddress = response.EmailAddress
            };
        }
    }
}
