using InventoryManagement.Application.Exceptions;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IServices;
using InventoryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = InventoryManagement.Application.Exceptions.ApplicationException;

namespace InventoryManagement.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<Guid> CreateUserAsync(Employee employee)
        {
            var newUser = new User()
            {
                EmployeeId = employee.Id,
                Email = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                UserName = employee.PersonnelCode
            };
            //create user
            var result = await userManager.CreateAsync(newUser);
            if (!result.Succeeded)
            {
                if (result.Errors.Any(e => e.Code.Contains("Duplicate", StringComparison.OrdinalIgnoreCase)))
                {
                    throw new DuplicateUserException();
                }
                throw new UserOperationFailedException();
            }
            //assign Employee role to every new user  
            var roleResult = await userManager.AddToRoleAsync(newUser, Roles.Employee);
            if (!roleResult.Succeeded)
            {
                throw new UserOperationFailedException();
            }
            return newUser.Id;
        }
    }
}
