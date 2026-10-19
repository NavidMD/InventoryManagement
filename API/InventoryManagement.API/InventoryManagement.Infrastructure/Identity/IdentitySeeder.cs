using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Identity
{
    public class IdentitySeeder
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IEmployeeRepository employeeRepository;

        public IdentitySeeder(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IEmployeeRepository employeeRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.employeeRepository = employeeRepository;
        }
        public async Task SeedAsync(CancellationToken cancellationToken)
        {
            //seeding roles
            var roles = new List<string>() { Roles.Admin, Roles.Employee };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var addRoleResult = await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                    if (!addRoleResult.Succeeded)
                    {
                        throw new InvalidOperationException("Failed to create identity role!");
                    }
                }
            }
            //finding admin registered info in dbo.Employees
            var adminEmployee = await employeeRepository.GetByPersonnelCode("392729618445", cancellationToken);
            if (adminEmployee == null)
            {
                throw new InvalidOperationException("The initial admin employee not found!");
            }
            //finding admin user in dbo.AspNetUsers
            var adminUser = await userManager.Users.FirstOrDefaultAsync(u => u.EmployeeId == adminEmployee.Id);
            if (adminUser == null)
            {
                var admin = new User()
                {
                    EmployeeId = adminEmployee.Id,
                    Email = adminEmployee.EmailAddress,
                    PhoneNumber = adminEmployee.PhoneNumber,
                    UserName = adminEmployee.PersonnelCode
                };
                //seeding admin user
                var createAdminUserResult = await userManager.CreateAsync(admin);
                if (!createAdminUserResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to create the initial admin user!");
                }
            }
            //assign role to admin user
            if (!await userManager.IsInRoleAsync(adminUser!, Roles.Admin))
            {
                var result = await userManager.AddToRoleAsync(
                    adminUser!, Roles.Admin);

                if (!result.Succeeded)
                    throw new InvalidOperationException(
                        "Failed to assign the Admin role to admin user");
            }
        }
    }
}
