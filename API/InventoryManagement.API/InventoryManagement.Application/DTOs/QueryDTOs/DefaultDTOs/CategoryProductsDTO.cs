using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs
{
    public class CategoryProductsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
    }
}
