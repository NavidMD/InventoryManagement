using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public EmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            context.Employees.Add(employee);
            await context.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await context.Employees.Include(e => e.Department).ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee?> UpdateAsync(Guid id, Employee updatedEmployee)
        {
            var employeeFoundForUpdate = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employeeFoundForUpdate != null)
            {
                context.Employees.Entry(employeeFoundForUpdate).CurrentValues.SetValues(updatedEmployee);
                await context.SaveChangesAsync();
                return employeeFoundForUpdate;
            }
            return null;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }


    }
}
