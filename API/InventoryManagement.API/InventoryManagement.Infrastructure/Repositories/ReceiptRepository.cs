using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Core.Models;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly AppDbContext context;

        public ReceiptRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Receipt> CreateAsync(Receipt receipt, CancellationToken cancellationToken)
        {
            context.Receipts.Add(receipt);
            await context.SaveChangesAsync();
            return receipt;
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {
            return await context.Receipts
                .Include(r => r.ReceiptItems)
                    .ThenInclude(ri => ri.Product)
                .Include(r => r.Employee)
                    .ThenInclude(re => re!.Department)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Receipt>> GetByDepartmentIdAsync(Guid departmentId)
        {
            return await context.Receipts
                .Where(r => r.Employee!.DepartmentId == departmentId)
                .Include(r => r.Employee)
                    .ThenInclude(ed => ed!.Department)
                .Include(r => r.ReceiptItems)
                    .ThenInclude(ri => ri.Product)
                .AsNoTracking()
                .ToListAsync(); 
        }

        public async Task<IEnumerable<Receipt>> GetByEmployeeIdAsync(Guid employeeId)
        {
            return await context.Receipts
                .Where(r => r.EmployeeId == employeeId)
                .Include(r => r.Employee)
                    .ThenInclude(e => e!.Department)
                .Include(r => r.ReceiptItems)
                    .ThenInclude(ri => ri.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Receipt?> GetByIdAsync(Guid id)
        {
            return context.Receipts
                .Include(r => r.ReceiptItems)
                    .ThenInclude(ri => ri.Product)
                .Include(r => r.Employee)
                    .ThenInclude(re => re!.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Receipt>> GetByProductIdAsync(Guid productId)
        {
            return await context.Receipts
               .Where(r => r.ReceiptItems.Any(ri => ri.ProductId == productId))
               .Include(r => r.ReceiptItems)
                   .ThenInclude(ri => ri.Product)
               .Include(r => r.Employee)
                   .ThenInclude(re => re!.Department)
               .AsNoTracking()
               .ToListAsync();
        }
    }
}
