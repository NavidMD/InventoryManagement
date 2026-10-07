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
    public record GetAllProductsQuery() : IRequest<IEnumerable<GetAllProductsResponseDTO>>;
    public class GetAllProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductsResponseDTO>>
    {
        public async Task<IEnumerable<GetAllProductsResponseDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var allProducts = await productRepository.GetAllAsync();
            return allProducts.Select(p => new GetAllProductsResponseDTO()
            {
                Id = p.Id,
                Title = p.Title,
                CategoryId = p.CategoryId,
                CategoryTitle = p.Category!.Title,
                SerialNumber = p.SerialNumber,
                StorageBalance = p.Inventory!.StorageBalance
            }).ToList();
        }
    }
}
