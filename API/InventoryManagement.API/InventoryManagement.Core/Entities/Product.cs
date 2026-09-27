using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(6)]
        public string SerialNumber { get; set; } = string.Empty;
        [Required]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
