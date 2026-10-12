using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.CommandDTOs.AuthCommandDTOs
{
    public class LoginResponseDTO
    {
        public bool OtpRequired { get; set; }
        public string Message { get; set; }
    }
}
