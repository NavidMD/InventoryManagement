using InventoryManagement.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Exceptions
{
    public sealed class UserOperationFailedException : ApplicationException
    {
        public UserOperationFailedException() : base("خطا در ایجاد حساب کاربری!", "CREATE_USER_FAILED") { }
    }
}
