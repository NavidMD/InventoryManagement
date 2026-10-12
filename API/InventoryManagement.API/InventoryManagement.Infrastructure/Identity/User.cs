using InventoryManagement.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Identity
{
    public class User : IdentityUser<Guid>
    {
        // Navigation Property برای دسترسی به اطلاعات کارمند
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        
        //اینها فیلدهایی هستند که بصورت داخلی در مدل IdentityUser وجود دارند و ارث بری شدن

        // public Guid Id { get; set; }

        // public string? UserName { get; set; }

        // public string? NormalizedUserName { get; set; }

        // public string? Email { get; set; }

        // public string? NormalizedEmail { get; set; }

        // public bool EmailConfirmed { get; set; }

        // public string? PasswordHash { get; set; }

        // public string? SecurityStamp { get; set; }

        // public string? ConcurrencyStamp { get; set; }
        // برای تشخیص تغییرات هم‌زمان در رکورد کاربر

        // public string? PhoneNumber { get; set; }

        // public bool PhoneNumberConfirmed { get; set; }

        // public bool TwoFactorEnabled { get; set; }

        // public DateTimeOffset? LockoutEnd { get; set; }

        // public bool LockoutEnabled { get; set; }

        // public int AccessFailedCount { get; set; }
    }
}
