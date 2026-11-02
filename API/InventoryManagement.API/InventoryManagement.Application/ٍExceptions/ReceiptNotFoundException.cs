using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Exceptions
{
    public sealed class ReceiptNotFoundException : ApplicationException
    {
        public ReceiptNotFoundException() : base("رسیدی با این شناسه یافت نشد!", "RECEIPT_NOT_FOUND")
        {
        }
    }
}
