using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.QueryDTOs.GetAllCategories
{
    public class GetAllCategoriesSubCategoryDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ICollection<GetAllCategoriesProductDTO> Products { get; set; } = new List<GetAllCategoriesProductDTO>();
        public ICollection<GetAllCategoriesSubCategoryDTO> SubCategories { get; set; } = new List<GetAllCategoriesSubCategoryDTO>();
    }
}
