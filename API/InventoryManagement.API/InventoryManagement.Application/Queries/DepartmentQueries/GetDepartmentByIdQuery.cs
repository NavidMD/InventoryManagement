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
    public record GetDepartmentByIdQuery(Guid departmentId) : IRequest<GetDepartmentByIdResponseDTO>;
    public class GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository) : IRequestHandler<GetDepartmentByIdQuery, GetDepartmentByIdResponseDTO>
    {
        public async Task<GetDepartmentByIdResponseDTO> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var foundedDepartment = await departmentRepository.GetByIdAsync(request.departmentId);
            if (foundedDepartment != null)
            {
                return new GetDepartmentByIdResponseDTO()
                {
                    Id = foundedDepartment.Id,
                    Title = foundedDepartment.Title,
                    Employees = foundedDepartment.Employees.Select(e => new DepartmentEmployeesDTO()
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        PersonnelCode = e.PersonnelCode,
                        DepartmentId = e.DepartmentId
                    }).ToList()
                };
            }
            throw new KeyNotFoundException("دپارتمان مورد نظر یافت نشد!");
        }
    }
}
