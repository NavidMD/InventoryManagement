using InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.ReceiptDTOs;
using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Core.Interfaces.Queries;
using InventoryManagement.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Queries.ReceiptQueries
{
    public record GetReceiptsByAdvancedFilterQuery(GetReceiptsByAdvancedFilterRequestDTO filters) : IRequest<IEnumerable<ReceiptReadModel>>;
    public class GetReceiptsByAdvancedFilterQueryHandler(IReceiptReadRepository receiptReadRepository) : IRequestHandler<GetReceiptsByAdvancedFilterQuery, IEnumerable<ReceiptReadModel>>
    {
        public async Task<IEnumerable<ReceiptReadModel>> Handle(GetReceiptsByAdvancedFilterQuery request, CancellationToken cancellationToken)
        {
            var filters = new ReceiptAdvancedFilterModel()
            {
                FromDate = request.filters.FromDate,
                ToDate = request.filters.ToDate,
                CategoryId = request.filters.CategoryId,
                DepartmentId = request.filters.DepartmentId
            };
            return await receiptReadRepository.GetByAdvancedFilterAsync(filters, cancellationToken);
        }
    }
}
