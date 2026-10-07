using InventoryManagement.Application.DTOs.QueryDTOs.ProductQueryDTOs;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Queries.ProductQueries
{
    public record GetProductByIdQuery(Guid productId) : IRequest<GetProductByIdResponseDTO>;
    public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, GetProductByIdResponseDTO>
    {
        public async Task<GetProductByIdResponseDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var foundProduct = await productRepository.GetByIdAsync(request.productId);
            if (foundProduct != null)
            {
                return new GetProductByIdResponseDTO()
                {
                    Id = foundProduct.Id,
                    Title = foundProduct.Title,
                    SerialNumber = foundProduct.SerialNumber,
                    CategoryId = foundProduct.CategoryId,
                    CategoryTitle = foundProduct.Category!.Title,
                    StorageBalance = foundProduct.Inventory!.StorageBalance
                };
            }
            throw new KeyNotFoundException("محصولی با این شناسه یافت نشد!");
        }
    }
}
