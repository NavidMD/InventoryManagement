using InventoryManagement.Application.DTOs.CommandDTOs.ReceiptItemCommandDTOs;
using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.CommandDTOs.ReceiptCommandDTOs
{
    public class CreateReceiptRequestDTO
    {
        public DateTime DateCreated { get; set; }
        public Guid EmployeeId { get; set; }
        public ICollection<CreateReceiptItemRequestDTO> ReceiptItems { get; set; } = new List<CreateReceiptItemRequestDTO>();
    }
}
