using InventoryManagement.Application.DTOs.QueryDTOs.EmployeeQueryDTOs;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Queries.EmployeeQueries
{
    public record GetEmployeeByIdQuery(Guid employeeId) : IRequest<GetEmployeeByIdResponseDTO>;
    public class GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponseDTO>
    {
        public async Task<GetEmployeeByIdResponseDTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var foundEmployee = await employeeRepository.GetByIdAsync(request.employeeId);
            if (foundEmployee != null)
            {
                return new GetEmployeeByIdResponseDTO()
                {
                    Id = foundEmployee.Id,
                    FirstName = foundEmployee.FirstName,
                    LastName = foundEmployee.LastName,
                    PersonnelCode = foundEmployee.PersonnelCode,
                    DepartmentId = foundEmployee.DepartmentId,
                    DepartmentTitle = foundEmployee.Department!.Title
                };
            }
            throw new KeyNotFoundException("کارمندی با این شناسه یافت نشد!");
        }
    }
}
