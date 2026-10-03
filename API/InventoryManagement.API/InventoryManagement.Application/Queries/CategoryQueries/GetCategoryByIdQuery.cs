using InventoryManagement.Application.DTOs.QueryDTOs.CategoryQueryDTOs;
using InventoryManagement.Application.DTOs.QueryDTOs.DefaultDTOs;
using InventoryManagement.Application.Mappers;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Queries.CategoryQueries
{
    public record GetCategoryByIdQuery(Guid id) : IRequest<GetCategoryByIdResponseDTO>;
    public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdResponseDTO>
    {
        public async Task<GetCategoryByIdResponseDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            //recursive CTE تغییر این بخش به
            var allCategories = await categoryRepository.GetAllAsync();
            var categoryFoundedById = await categoryRepository.GetByIdAsync(request.id);
            if (categoryFoundedById != null)
            {
                var response = new GetCategoryByIdResponseDTO()
                {
                    Id = categoryFoundedById.Id,
                    Title = categoryFoundedById.Title,
                    Products = categoryFoundedById.Products.Select(product => new CategoryProductsDTO()
                    {
                        Id = product.Id,
                        Title = product.Title,
                        SerialNumber = product.SerialNumber
                    }).ToList(),
                    ParentCategoryId = categoryFoundedById.ParentCategoryId,
                    SubCategories = allCategories
                        .Where(c => c.ParentCategoryId == categoryFoundedById.Id)
                        .Select(subCategory => CategoryTreeMapper.MapSubCategory(subCategory, allCategories))
                        .ToList()
                };
                return response;
            }
            throw new KeyNotFoundException("دسته بندی مورد نظر یافت نشد!");
        }
    }
}
