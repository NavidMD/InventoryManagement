using InventoryManagement.Application.Abstractions;
using InventoryManagement.Application.DTOs.CommandDTOs.ReceiptCommandDTOs;
using InventoryManagement.Application.DTOs.CommandDTOs.ReceiptItemCommandDTOs;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.ReceiptCommands
{
    public record CreateReceiptCommand(CreateReceiptRequestDTO newReceipt) : IRequest<CreateReceiptResponseDTO>;
    public class CreateReceiptCommandHandler(IReceiptRepository receiptRepository, IProductRepository productRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateReceiptCommand, CreateReceiptResponseDTO>
    {
        public async Task<CreateReceiptResponseDTO> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
        {
            //new receipt
            var receipt = new Receipt()
            {
                DateCreated = DateTime.UtcNow,
                EmployeeId = request.newReceipt.EmployeeId,
                ReceiptItems = request.newReceipt.ReceiptItems.Select(ri => new ReceiptItem()
                {
                    ProductId = ri.ProductId,
                    Quantity = ri.Quantity
                }).ToList()
            };
            //begin transaction
            try
            {
                await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);
                //checking each item inventory then decreasing
                foreach (var item in receipt.ReceiptItems)
                {
                    var changedRows = await productRepository.DecreaseInventoryAsync(item.ProductId, item.Quantity, cancellationToken);
                    if (changedRows == 0)
                    {
                        throw new InsufficientInventoryException();
                    }
                }
                //submitting receipt
                var createdReceipt = await receiptRepository.CreateAsync(receipt, cancellationToken);
                var response = new CreateReceiptResponseDTO()
                {
                    Id = createdReceipt.Id,
                    ReceiptNumber = createdReceipt.ReceiptNumber,
                    DateCreated = createdReceipt.DateCreated,
                    EmployeeId = createdReceipt.EmployeeId,
                    ReceiptItems = createdReceipt.ReceiptItems.Select(ri => new CreateReceiptItemResponseDTO()
                    {
                        Id = ri.Id,
                        ProductId = ri.ProductId,
                        ReceiptId = ri.ReceiptId,
                        Quantity = ri.Quantity
                    }).ToList()
                };

                await unitOfWork.CommitAsync();
                return response;
            }
            catch
            {
                await unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
