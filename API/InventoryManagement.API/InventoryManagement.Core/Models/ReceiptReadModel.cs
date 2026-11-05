using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Models
{
    public class ReceiptReadModel
    {
        public Guid Id { get; set; }
        public int ReceiptNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string DepartmentTitle { get; set; } = string.Empty;
        public ICollection<ReceiptItemReadModel> ReceiptItems { get; set; } = new List<ReceiptItemReadModel>();
    }

    public class ReceiptItemReadModel
    {
        public Guid Id { get; set; }
        public Guid ReceiptId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductTitle { get; set; } = string.Empty;
        public string ProductSerialNumber { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
