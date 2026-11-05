using InventoryManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.Queries
{
    public interface IReceiptReadRepository
    {
        Task<IEnumerable<ReceiptReadModel>> GetByAdvancedFilterAsync(ReceiptAdvancedFilterModel filters, CancellationToken cancellationToken);
    }
}
