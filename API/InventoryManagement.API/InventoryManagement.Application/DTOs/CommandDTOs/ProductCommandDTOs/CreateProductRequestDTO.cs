using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.CommandDTOs.ProductCommandDTOs
{
    public class CreateProductRequestDTO
    {
        public string Title { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public int InitialStock { get; set; }
    }
}
