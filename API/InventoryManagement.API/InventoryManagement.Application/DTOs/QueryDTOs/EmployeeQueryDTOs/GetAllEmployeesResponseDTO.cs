using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.QueryDTOs.EmployeeQueryDTOs
{
    public class GetAllEmployeesResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PersonnelCode { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public string DepartmentTitle { get; set; } = string.Empty;
    }
}
