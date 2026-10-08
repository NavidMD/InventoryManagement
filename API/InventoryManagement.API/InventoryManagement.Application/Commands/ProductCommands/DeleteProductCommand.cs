using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.ProductCommands
{
    public record DeleteProductCommand(Guid productId) : IRequest<bool>;
    public class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = await productRepository.DeleteAsync(request.productId);
            if(!response)
            {
                throw new KeyNotFoundException("محصولی با این شناسه جهت حذف یافت نشد!");
            }
            return response;
        }
    }
}
