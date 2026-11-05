using InventoryManagement.Core.Interfaces.Queries;
using InventoryManagement.Core.Models;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Queries
{
    public class ReceiptReadRepository : IReceiptReadRepository
    {
        private readonly AppDbContext context;
        public ReceiptReadRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<ReceiptReadModel>> GetByAdvancedFilterAsync(ReceiptAdvancedFilterModel filters, CancellationToken cancellationToken)
        {
            return await context.Receipts
                .AsNoTracking()
                .Where(r => filters.DepartmentId == null || r.Employee!.DepartmentId == filters.DepartmentId)
                .Where(r => filters.CategoryId == null || r.ReceiptItems.Any(ri => ri.Product!.CategoryId == filters.CategoryId))
                .Where(r => filters.FromDate == null || r.DateCreated >= filters.FromDate)
                .Where(r => filters.ToDate == null || r.DateCreated <= filters.ToDate)
                .Select(r => new ReceiptReadModel()
                {
                    Id = r.Id,
                    DateCreated = r.DateCreated,
                    ReceiptNumber = r.ReceiptNumber,
                    EmployeeId = r.EmployeeId,
                    EmployeeName = String.Concat(r.Employee!.FirstName + " " + r.Employee!.LastName),
                    DepartmentTitle = r.Employee!.Department!.Title,
                    ReceiptItems = r.ReceiptItems.Select(ri => new ReceiptItemReadModel()
                    {
                        Id = ri.Id,
                        ProductId = ri.ProductId,
                        ProductTitle = ri.Product!.Title,
                        ProductSerialNumber = ri.Product!.SerialNumber,
                        ReceiptId = ri.ReceiptId,
                        Quantity = ri.Quantity
                    }).ToList()
                })
                .ToListAsync(cancellationToken);
        }
    }
}
