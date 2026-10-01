using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.CommandDTOs.CategoryCommandDTOs
{
    public class CreateCategoryRequestDTO
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
    }
}
