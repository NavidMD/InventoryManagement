using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.CommandDTOs.AuthCommandDTOs
{
    public class VertificationRequestDTO
    {
        [Required]
        [StringLength(6)]
        public string OtpCode { get; set; } = string.Empty;
    }
}
