using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs
{
    public class CategoryTreeDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ICollection<CategoryProductsDTO> Products { get; set; } = new List<CategoryProductsDTO>();
        public ICollection<CategoryTreeDTO> SubCategories { get; set; } = new List<CategoryTreeDTO>();
    }
}
