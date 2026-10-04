using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.CommandDTOs.DepartmentCommandDTOs
{
    public class UpdateDepartmentRequestDTO
    {
        public string Title { get; set; } = string.Empty;
    }
}
