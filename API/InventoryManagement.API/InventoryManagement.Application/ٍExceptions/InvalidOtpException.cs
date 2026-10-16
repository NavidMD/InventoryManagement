using InventoryManagement.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = InventoryManagement.Application.Exceptions.ApplicationException;

namespace InventoryManagement.Application._ٍExceptions
{
    public sealed class InvalidOtpException : ApplicationException
    {
        public InvalidOtpException() : base("کد وارد شده معتبر نیست!", "INVALID_OTP")
        {
            
        }
    }
}
