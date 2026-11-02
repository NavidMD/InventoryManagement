using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs
{
    public class ReceiptItemDTO
    {
        public Guid Id { get; set; }
        public Guid ReceiptId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductTitle { get; set; } = string.Empty;
        public string ProductSerialNumber { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
