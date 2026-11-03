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
    public record GetReceiptsByDepartmentIdQuery(Guid departmentId) : IRequest<IEnumerable<GetReceiptsByDepartmentIdResponseDTO>>;
    public class GetReceiptsByDepartmentIdQueryHandler(IReceiptRepository receiptRepository) : IRequestHandler<GetReceiptsByDepartmentIdQuery, IEnumerable<GetReceiptsByDepartmentIdResponseDTO>>
    {
        public async Task<IEnumerable<GetReceiptsByDepartmentIdResponseDTO>> Handle(GetReceiptsByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            var response = await receiptRepository.GetByDepartmentIdAsync(request.departmentId);

            return response.Select(r => new GetReceiptsByDepartmentIdResponseDTO()
            {
                Id = r.Id,
                DateCreated = r.DateCreated,
                EmployeeId = r.EmployeeId,
                EmployeeName = String.Concat(r.Employee!.FirstName, " ", r.Employee!.LastName),
                DepartmentTitle = r.Employee!.Department!.Title,
                ReceiptNumber = r.ReceiptNumber,
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
