using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.CommandDTOs.AuthCommandDTOs
{
    public class LoginRequestDTO
    {
        public string PersonnelCode { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }
}
