using InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.DepartmentQueryDTOs;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Queries.DepartmentQueries
{
    public record GetAllDepartmentsQuery() : IRequest<IEnumerable<GetAllDepartmentsResponseDTO>>;
    public class GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository) : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<GetAllDepartmentsResponseDTO>>
    {
        public async Task<IEnumerable<GetAllDepartmentsResponseDTO>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var allDepartments = await departmentRepository.GetAllAsync();
            return allDepartments.Select(d => new GetAllDepartmentsResponseDTO()
            {
                Id = d.Id,
                Title = d.Title,
                Employees = d.Employees.Select(e => new DepartmentEmployeesDTO()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PersonnelCode = e.PersonnelCode,
                    DepartmentId = e.DepartmentId
                }).ToList()
            }).ToList();
        }
    }
}
