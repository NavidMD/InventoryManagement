using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Models
{
    public class ReceiptAdvancedFilterModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
