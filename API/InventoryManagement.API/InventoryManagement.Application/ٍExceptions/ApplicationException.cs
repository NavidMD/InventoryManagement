using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        public string ErrorCode { get; }
        protected ApplicationException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
