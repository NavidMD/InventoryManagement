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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext context;

        public DepartmentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Department> CreateAsync(Department department)
        {
            context.Departments.Add(department);
            await context.SaveChangesAsync();
            return department;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await context.Departments.Include(d => d.Employees).ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(Guid id)
        {
            return await context.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Department?> UpdateAsync(Guid id, Department updatedDepartment)
        {
            var departmentFoundForUpdate = await context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (departmentFoundForUpdate != null)
            {
                departmentFoundForUpdate.Title = updatedDepartment.Title;
                await context.SaveChangesAsync();
                return departmentFoundForUpdate;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var rowDeleted = await context.Departments
                .Where(d => d.Id == id)
                .ExecuteDeleteAsync();
            return rowDeleted > 0;
        }
    }
}
