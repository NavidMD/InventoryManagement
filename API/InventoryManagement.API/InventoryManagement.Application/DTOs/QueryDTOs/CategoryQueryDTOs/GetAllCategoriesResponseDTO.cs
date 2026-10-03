using InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs;
using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.QueryDTOs.CategoryQueryDTOs
{
    public class GetAllCategoriesResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ICollection<CategoryProductsDTO> Products { get; set; } = new List<CategoryProductsDTO>();
        public Guid? ParentCategoryId { get; set; }
        public ICollection<CategoryTreeDTO> SubCategories { get; set; } = new List<CategoryTreeDTO>();
    }
}
