using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [StringLength(12)]
        public string PersonnelCode { get; set; } = string.Empty;
        [Required]
        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<Receipt>? Receipts { get; set; }
    }
}
