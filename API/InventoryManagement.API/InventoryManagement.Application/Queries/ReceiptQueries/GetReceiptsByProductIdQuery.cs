using InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.ReceiptDTOs;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Queries.ReceiptQueries
{
    public record GetReceiptsByProductIdQuery(Guid productId) : IRequest<IEnumerable<GetReceiptsByProductIdResponseDTO>>;
    public class GetReceiptsByProductIdQueryHandler(IReceiptRepository receiptRepository) : IRequestHandler<GetReceiptsByProductIdQuery, IEnumerable<GetReceiptsByProductIdResponseDTO>>
    {
        public async Task<IEnumerable<GetReceiptsByProductIdResponseDTO>> Handle(GetReceiptsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var response = await receiptRepository.GetByProductIdAsync(request.productId);
            return response.Select(r => new GetReceiptsByProductIdResponseDTO()
            {
                Id = r.Id,
                DateCreated = r.DateCreated,
                ReceiptNumber = r.ReceiptNumber,
                EmployeeId = r.EmployeeId,
                EmployeeName = String.Concat(r.Employee!.FirstName, " ", r.Employee!.LastName),
                DepartmentTitle = r.Employee!.Department!.Title,
                ReceiptItems = r.ReceiptItems.Select(ri => new ReceiptItemDTO()
                {
                    Id = ri.Id,
                    ProductId = ri.ProductId,
                    ProductSerialNumber = ri.Product!.SerialNumber,
                    ProductTitle = ri.Product!.Title,
                    ReceiptId = ri.ReceiptId,
                    Quantity = ri.Quantity
                }).ToList()
            }).ToList();
        }
    }
}
