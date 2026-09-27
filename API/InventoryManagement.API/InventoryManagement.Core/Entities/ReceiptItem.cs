using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Entities
{
    public class ReceiptItem
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid ReceiptId { get; set; }
        public Receipt? Receipt { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
