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
    public class CreateReceiptResponseDTO
    {
        public Guid Id { get; set; }
        public int ReceiptNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid EmployeeId { get; set; }
        public ICollection<CreateReceiptItemResponseDTO> ReceiptItems { get; set; } = new List<CreateReceiptItemResponseDTO>();
    }
}
