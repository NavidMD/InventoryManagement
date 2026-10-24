using InventoryManagement.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Exceptions
{
    public sealed class TokenOperationFailedException : ApplicationException
    {
        public TokenOperationFailedException() : base("خطا در اعتبارسنجی توکن", "TOKEN_OPERATION_FAILED") { }
    }
}
