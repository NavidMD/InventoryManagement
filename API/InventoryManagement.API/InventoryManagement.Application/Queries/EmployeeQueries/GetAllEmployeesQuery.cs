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
    public record GetAllEmployeesQuery() : IRequest<IEnumerable<GetAllEmployeesResponseDTO>>;
    public class GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetAllEmployeesQuery, IEnumerable<GetAllEmployeesResponseDTO>>
    {
        public async Task<IEnumerable<GetAllEmployeesResponseDTO>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var allEmployees = await employeeRepository.GetAllAsync();
            return allEmployees.Select(e => new GetAllEmployeesResponseDTO()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                DepartmentId = e.DepartmentId,
                PersonnelCode = e.PersonnelCode,
                DepartmentTitle = e.Department!.Title
            }).ToList();
        }
    }
}
