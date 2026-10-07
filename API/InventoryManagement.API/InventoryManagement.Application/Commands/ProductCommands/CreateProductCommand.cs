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
    public record CreateProductCommand(CreateProductRequestDTO newProduct) : IRequest<CreateProductResponseDTO>;
    public class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, CreateProductResponseDTO>
    {
        public async Task<CreateProductResponseDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Title = request.newProduct.Title,
                SerialNumber = request.newProduct.SerialNumber,
                CategoryId = request.newProduct.CategoryId,
                Inventory = new Inventory()
                {
                    StorageBalance = request.newProduct.InitialStock
                }
            };
            var response = await productRepository.CreateAsync(product);
            return new CreateProductResponseDTO()
            {
                Id = response.Id,
                Title = response.Title,
                SerialNumber = response.SerialNumber,
                CategoryId = response.CategoryId,
                InitailStock = response.Inventory!.StorageBalance
            };
        }
    }
}
