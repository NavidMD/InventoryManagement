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
    public record GetAllReceiptsQuery() : IRequest<IEnumerable<GetAllReceiptsResponseDTO>>;
    public class GetAllReceiptsQueryHandler(IReceiptRepository receiptRepository) : IRequestHandler<GetAllReceiptsQuery, IEnumerable<GetAllReceiptsResponseDTO>>
    {
        public async Task<IEnumerable<GetAllReceiptsResponseDTO>> Handle(GetAllReceiptsQuery request, CancellationToken cancellationToken)
        {
            var response = await receiptRepository.GetAllAsync();
            return response.Select(r => new GetAllReceiptsResponseDTO()
            {
                Id = r.Id,
                DateCreated = r.DateCreated,
                ReceiptNumber = r.ReceiptNumber,
                EmployeeId = r.EmployeeId,
                EmployeeName = String.Concat(r.Employee!.FirstName, " ", r.Employee!.LastName),
                DepartmentTitle = r.Employee.Department!.Title,
                ReceiptItems = r.ReceiptItems.Select(ri => new ReceiptItemDTO()
                {
                    Id = ri.Id,
                    ProductId = ri.ProductId,
                    ProductTitle = ri.Product!.Title,
                    ProductSerialNumber = ri.Product!.SerialNumber,
                    Quantity = ri.Quantity,
                    ReceiptId = ri.ReceiptId
                }).ToList()
            });
        }
    }
}
