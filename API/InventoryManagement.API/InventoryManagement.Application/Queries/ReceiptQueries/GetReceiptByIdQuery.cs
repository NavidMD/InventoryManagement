using InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.ReceiptDTOs;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Queries.ReceiptQueries
{
    public record GetReceiptByIdQuery(Guid receiptId) : IRequest<GetReceiptByIdResponseDTO>;
    public class GetReceiptByIdQueryHandler(IReceiptRepository receiptRepository) : IRequestHandler<GetReceiptByIdQuery, GetReceiptByIdResponseDTO>
    {
        public async Task<GetReceiptByIdResponseDTO> Handle(GetReceiptByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await receiptRepository.GetByIdAsync(request.receiptId);
            if (response != null)
            {
                return new GetReceiptByIdResponseDTO()
                {
                    Id = response.Id,
                    DateCreated = response.DateCreated,
                    ReceiptNumber = response.ReceiptNumber,
                    EmployeeId = response.EmployeeId,
                    EmployeeName = String.Concat(response.Employee!.FirstName + " " + response.Employee!.LastName),
                    DepartmentTitle = response.Employee.Department!.Title,
                    ReceiptItems = response.ReceiptItems.Select(ri => new ReceiptItemDTO()
                    {
                        Id = ri.Id,
                        ProductId = ri.ProductId,
                        ProductTitle = ri.Product!.Title,
                        ProductSerialNumber = ri.Product!.SerialNumber,
                        ReceiptId = ri.ReceiptId,
                        Quantity = ri.Quantity
                    }).ToList()
                };
            }
            throw new ReceiptNotFoundException();
        }
    }
}
