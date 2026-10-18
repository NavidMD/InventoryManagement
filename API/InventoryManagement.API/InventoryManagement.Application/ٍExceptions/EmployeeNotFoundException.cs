using InventoryManagement.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Exceptions
{
    public sealed class EmployeeNotFoundException : ApplicationException
    {
        public EmployeeNotFoundException() : base("کارمندی با این شناسه یافت نشد!", "EMPLOYEE_NOT_FOUND") { }
    }
}
