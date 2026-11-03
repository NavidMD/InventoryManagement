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
    public record GetReceiptsByEmployeeIdQuery(Guid employeeId) : IRequest<IEnumerable<GetReceiptsByEmployeeIdResponseDTO>>;
    public class GetReceiptsByEmployeeIdQueryHandler(IReceiptRepository receiptRepository) : IRequestHandler<GetReceiptsByEmployeeIdQuery, IEnumerable<GetReceiptsByEmployeeIdResponseDTO>>
    {
        public async Task<IEnumerable<GetReceiptsByEmployeeIdResponseDTO>> Handle(GetReceiptsByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var response = await receiptRepository.GetByEmployeeIdAsync(request.employeeId);
            return response.Select(r => new GetReceiptsByEmployeeIdResponseDTO()
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
