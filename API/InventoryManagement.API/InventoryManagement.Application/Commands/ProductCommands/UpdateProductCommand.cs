using InventoryManagement.Application.DTOs.CommandDTOs.ProductCommandDTOs;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.ProductCommands
{
    public record UpdateProductCommand(Guid productId, UpdateProductRequestDTO updatedProduct) : IRequest<UpdateProductResponseDTO>;
    public class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, UpdateProductResponseDTO>
    {
        public async Task<UpdateProductResponseDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new Product()
            {
                Id = request.productId,
                Title = request.updatedProduct.Title,
                CategoryId = request.updatedProduct.CategoryId,
                SerialNumber = request.updatedProduct.SerialNumber,
                Inventory = new Inventory()
                {
                    StorageBalance = request.updatedProduct.StorageBalance,
                },
            };
            var response = await productRepository.UpdateAsync(request.productId, newProduct);
            if (response != null)
            {
                return new UpdateProductResponseDTO()
                {
                    Id = response.Id,
                    Title = response.Title,
                    CategoryId = response.CategoryId,
                    SerialNumber = response.SerialNumber,
                    StorageBalance = response.Inventory!.StorageBalance
                };
            }
            throw new KeyNotFoundException("محصولی با این شناسه جهت ویرایش یافت نشد!");
        }
    }
}
