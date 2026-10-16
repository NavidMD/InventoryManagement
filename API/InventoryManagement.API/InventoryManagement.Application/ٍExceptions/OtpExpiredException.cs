using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Exceptions
{
    public sealed class OtpExpiredException : ApplicationException
    {
        public OtpExpiredException() : base("کد تایید منقضی شده است!", "OTP_EXPIRED")
        {
            
        }
    }
}
