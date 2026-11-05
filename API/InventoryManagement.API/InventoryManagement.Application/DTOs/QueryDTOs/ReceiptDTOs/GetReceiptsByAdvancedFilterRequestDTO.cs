using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs.QueryDTOs.ReceiptDTOs
{
    public class GetReceiptsByAdvancedFilterRequestDTO
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
