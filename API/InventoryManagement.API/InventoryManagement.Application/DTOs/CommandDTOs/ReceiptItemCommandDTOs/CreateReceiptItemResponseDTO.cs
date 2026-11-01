using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.CommandDTOs.ReceiptItemCommandDTOs
{
    public class CreateReceiptItemResponseDTO
    {
        public Guid Id { get; set; }
        public Guid ReceiptId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
