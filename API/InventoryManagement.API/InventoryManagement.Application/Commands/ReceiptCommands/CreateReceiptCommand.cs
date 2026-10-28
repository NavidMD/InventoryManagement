using InventoryManagement.Application.DTOs.CommandDTOs.ReceiptCommandDTOs;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.ReceiptCommands
{
    public record CreateReceiptCommand(CreateReceiptRequestDTO newReceipt) : IRequest<CreateReceiptResponseDTO>;
    public class CreateReceiptCommandHandler(IReceiptRepository receiptRepository) : IRequestHandler<CreateReceiptCommand, CreateReceiptResponseDTO>
    {
        public Task<CreateReceiptResponseDTO> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
