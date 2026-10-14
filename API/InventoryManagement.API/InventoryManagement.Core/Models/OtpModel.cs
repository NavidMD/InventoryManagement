using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Models
{
    public class OtpModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string OtpHash { get; set; } = string.Empty;
        public DateTime ExpireTime { get; set; } = DateTime.UtcNow.AddMinutes(5);
        [Required]
        public Guid EmployeeId { get; set; }
        public int FailedAttempts { get; set; }
        public bool IsUsed { get; set; }
        public bool IsActive { get; set; }
    }
}
