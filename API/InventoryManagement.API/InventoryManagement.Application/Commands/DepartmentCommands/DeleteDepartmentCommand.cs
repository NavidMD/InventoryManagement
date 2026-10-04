using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.DepartmentCommands
{
    public record DeleteDepartmentCommand(Guid departmentId) : IRequest<bool>;
    public class DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository) : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var response = await departmentRepository.DeleteAsync(request.departmentId);
            if (!response)
            {
                throw new KeyNotFoundException("دپارتمان با این شناسه جهت حذف یافت نشد!");
            }
            return response;
        }
    }
}
