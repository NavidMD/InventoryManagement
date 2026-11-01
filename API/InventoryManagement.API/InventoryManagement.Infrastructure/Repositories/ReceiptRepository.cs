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
                .ToListAsync();
        }

        public Task<Receipt> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
