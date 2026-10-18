using InventoryManagement.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Exceptions
{
    public sealed class DuplicateUserException : ApplicationException
    {
        public DuplicateUserException() : base("این کاربر قبلا ثبت شده است!", "DUPLICATE_USER") { }
    }
}
