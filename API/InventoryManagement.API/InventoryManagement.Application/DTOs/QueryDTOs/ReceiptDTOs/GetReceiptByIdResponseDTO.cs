using InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs;
using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.QueryDTOs.ReceiptDTOs
{
    public class GetReceiptByIdResponseDTO
    {
        public Guid Id { get; set; }
        public int ReceiptNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string DepartmentTitle { get; set; } = string.Empty;
        public ICollection<ReceiptItemDTO> ReceiptItems { get; set; } = new List<ReceiptItemDTO>();
    }
}
