using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.QueryDTOs.GetAllCategories
{
    public class GetAllCategoriesProductDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
    }
}
