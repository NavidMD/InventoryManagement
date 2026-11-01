using InventoryManagement.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Exceptions
{
    public sealed class InsufficientInventoryException : ApplicationException
    {
        public InsufficientInventoryException() : base("موجودی محصول کافی نیست!", "INSUFFICIENT_INVENTORY")
        {
            
        }
    }
}
