using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.EmployeeCommands
{
    public record DeleteEmployeeCommand(Guid employeeId) : IRequest<bool>;
    public class DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository) : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = await employeeRepository.DeleteAsync(request.employeeId);
            if (!response)
            {
                throw new KeyNotFoundException("کارمندی با این شناسه جهت حذف یافت نشد!");
            }
            return response;
        }
    }
}
