using InventoryManagement.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Exceptions
{
    public sealed class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException() : base("حساب کاربری یافت نشد!", "USER_NOT_FOUND") { }
    }
}
