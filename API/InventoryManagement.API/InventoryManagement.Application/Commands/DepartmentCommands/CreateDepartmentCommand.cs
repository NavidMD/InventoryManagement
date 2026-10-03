using InventoryManagement.Application.DTOs.CommandDTOs.DepartmentCommandDTOs;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.DepartmentCommands
{
    public record CreateDepartmentCommand(CreateDepartmentRequestDTO newDepartment) : IRequest<CreateDepartmentResponseDTO>;
    public class CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository) : IRequestHandler<CreateDepartmentCommand, CreateDepartmentResponseDTO>
    {
        public async Task<CreateDepartmentResponseDTO> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = new Department()
            {
                Title = request.newDepartment.Title
            };
            var response = await departmentRepository.CreateAsync(department);
            return new CreateDepartmentResponseDTO()
            {
                Id = response.Id,
                Title = response.Title
            };
        }
    }
}
