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
    public record UpdateDepartmentCommand(Guid departmentId, UpdateDepartmentRequestDTO updatedDepartment) : IRequest<UpdateDepartmentResponseDTO>;
    public class UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository) : IRequestHandler<UpdateDepartmentCommand, UpdateDepartmentResponseDTO>
    {
        public async Task<UpdateDepartmentResponseDTO> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var newDepartment = new Department()
            {
                Id = request.departmentId,
                Title = request.updatedDepartment.Title,
            };
            var response = await departmentRepository.UpdateAsync(request.departmentId, newDepartment);
            if (response == null)
            {
                throw new KeyNotFoundException("دپارتمان با این شناسه جهت ویرایش یافت نشد!");
            }
            return new UpdateDepartmentResponseDTO()
            {
                Id = response.Id,
                Title = response.Title
            };
        }
    }
}
