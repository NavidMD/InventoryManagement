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

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task<Department?> UpdateAsync(Guid id, Department updatedDepartment)
        {
            throw new NotImplementedException();
        }
    }
}
