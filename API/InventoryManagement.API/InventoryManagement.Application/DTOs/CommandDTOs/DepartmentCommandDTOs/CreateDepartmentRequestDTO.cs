using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.CommandDTOs.DepartmentCommandDTOs
{
    public class CreateDepartmentRequestDTO
    {
        public string Title { get; set; } = string.Empty;
    }
}
