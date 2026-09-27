using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Entities
{
    public class Receipt
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int ReceiptNumber { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<ReceiptItem> ReceiptItems { get; set; } = new List<ReceiptItem>();
    }
}
