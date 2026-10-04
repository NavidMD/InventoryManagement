using InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs;
using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.QueryDTOs.DepartmentQueryDTOs
{
    public class GetDepartmentByIdResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ICollection<DepartmentEmployeesDTO> Employees { get; set; } = new List<DepartmentEmployeesDTO>();
    }
}
